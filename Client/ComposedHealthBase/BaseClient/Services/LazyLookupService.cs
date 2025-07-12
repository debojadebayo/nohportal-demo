using System.Net.Http.Json;
using MudBlazor;
using ComposedHealthBase.Shared.Interfaces;
using ComposedHealthBase.Shared.DTOs;
using System.Text;
using System.Collections.Concurrent;

namespace ComposedHealthBase.BaseClient.Services
{
    public interface ILazyLookupService<TDto>
where TDto : IDto, ILazyLookup
    {
        string ItemToString(Guid e);
        Task<IEnumerable<Guid>> ItemSearch(string value, CancellationToken token, Guid? tenantConstraint = null, Guid? subjectConstraint = null);
        Task<TDto?> GetItemById(Guid id, CancellationToken token, Guid? tenantConstraint = null, Guid? subjectConstraint = null, bool forceUpdate = false);
        Task<IEnumerable<TDto>> GetItemsByIds(IEnumerable<Guid> ids, CancellationToken token, Guid? tenantConstraint = null, Guid? subjectConstraint = null, bool forceUpdate = false);
        Task<IEnumerable<TDto>> GetAllItems(CancellationToken token, Guid? tenantConstraint = null, Guid? subjectConstraint = null);
        Task AddItem(TDto item, CancellationToken token);
        Task UpdateItem(TDto item, CancellationToken token);
        Task DeleteItem(Guid id, CancellationToken token);
        Task<IEnumerable<TDto>> GetAllByTenantId(Guid tenantId, CancellationToken token);
        Task<IEnumerable<TDto>> GetAllBySubjectId(Guid subjectId, CancellationToken token);
        Task<IEnumerable<TDto>> GetAllByCustom(string customRoute, CancellationToken token);
    }

    public class LazyLookupService<TDto> : ILazyLookupService<TDto>
    where TDto : IDto, ILazyLookup
    {
        private static string EndpointType => typeof(TDto).Name.Replace("Dto", string.Empty).ToLowerInvariant();
        private Dictionary<Guid, Tuple<TDto, DateTime>> _itemList { get; set; } = new();
        private readonly ConcurrentDictionary<string, Task> _ongoingRequests = new();
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;
        public LazyLookupService(HttpClient httpClient, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
        }

        public string ItemToString(Guid e) => e != Guid.Empty && _itemList.TryGetValue(e, out var item) ? $"{item.Item1.DisplayName}" : string.Empty;
        public async Task<IEnumerable<Guid>> ItemSearch(string value, CancellationToken token, Guid? tenantConstraint = null, Guid? subjectConstraint = null)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            {
                return default!;
            }

            var key = $"search_{value}_{tenantConstraint}_{subjectConstraint}";
            
            if (_ongoingRequests.TryGetValue(key, out var existingTask) && existingTask is Task<IEnumerable<Guid>> existingSearchTask)
            {
                return await existingSearchTask;
            }

            var task = PerformItemSearch(value, token, tenantConstraint, subjectConstraint);
            _ongoingRequests[key] = task;

            try
            {
                return await task;
            }
            finally
            {
                _ongoingRequests.TryRemove(key, out _);
            }
        }

        private async Task<IEnumerable<Guid>> PerformItemSearch(string value, CancellationToken token, Guid? tenantConstraint, Guid? subjectConstraint)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"api/{EndpointType}/search?term=" + value);
                if (tenantConstraint.HasValue)
                {
                    sb.Append($"&tenantId={tenantConstraint.Value}");
                }
                if (subjectConstraint.HasValue)
                {
                    sb.Append($"&subjectId={subjectConstraint.Value}");
                }

                var response = await _httpClient.GetAsync(sb.ToString(), token);
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return Enumerable.Empty<Guid>();
                
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<TDto>>(cancellationToken: token);
                if (result == null || _itemList == null)
                {
                    return Enumerable.Empty<Guid>();
                }
                foreach (var item in result)
                {
                    _itemList.TryAdd(item.Id, new Tuple<TDto, DateTime>(item, DateTime.UtcNow));
                }
                return result?.Select(x => x.Id) ?? Enumerable.Empty<Guid>();
            }
            catch (OperationCanceledException)
            {
                // Request was cancelled (likely due to new search request), don't show error
                return Enumerable.Empty<Guid>();
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Failed to search items: {ex.Message}", Severity.Error);
                return Enumerable.Empty<Guid>();
            }
        }

        public async Task<TDto?> GetItemById(Guid id, CancellationToken token, Guid? tenantConstraint, Guid? subjectConstraint, bool forceUpdate = false)
        {
            if (_itemList.TryGetValue(id, out var item) && !forceUpdate)
            {
                if (item.Item2.AddMinutes(1) > DateTime.UtcNow)
                {
                    return item.Item1;
                }
            }

            var key = $"getbyid_{id}_{tenantConstraint}_{subjectConstraint}_{forceUpdate}";
            
            if (_ongoingRequests.TryGetValue(key, out var existingTask) && existingTask is Task<TDto?> existingGetTask)
            {
                return await existingGetTask;
            }

            var task = PerformGetItemById(id, token, tenantConstraint, subjectConstraint);
            _ongoingRequests[key] = task;

            try
            {
                return await task;
            }
            finally
            {
                _ongoingRequests.TryRemove(key, out _);
            }
        }

        private async Task<TDto?> PerformGetItemById(Guid id, CancellationToken token, Guid? tenantConstraint, Guid? subjectConstraint)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"api/{EndpointType}/getbyid/{id}?1=1");
                if (tenantConstraint.HasValue)
                {
                    sb.Append($"&tenantId={tenantConstraint.Value}");
                }
                if (subjectConstraint.HasValue)
                {
                    sb.Append($"&subjectId={subjectConstraint.Value}");
                }
                var response = await _httpClient.GetAsync(sb.ToString(), token);
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return default;
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<TDto>(cancellationToken: token);
                if (result != null)
                {
                    _itemList.TryAdd(id, new Tuple<TDto, DateTime>(result, DateTime.UtcNow));
                }
                return result;
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Failed to get item by ID: {ex.Message}", Severity.Error);
                return default;
            }
        }

        public async Task<IEnumerable<TDto>> GetItemsByIds(IEnumerable<Guid> ids, CancellationToken token, Guid? tenantConstraint, Guid? subjectConstraint, bool forceUpdate = false)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<TDto>();
            }

            var idsArray = ids.ToArray();
            var key = $"getbyids_{string.Join(",", idsArray.OrderBy(x => x))}_{tenantConstraint}_{subjectConstraint}_{forceUpdate}";
            
            if (_ongoingRequests.TryGetValue(key, out var existingTask) && existingTask is Task<IEnumerable<TDto>> existingGetTask)
            {
                return await existingGetTask;
            }

            var task = PerformGetItemsByIds(idsArray, token, tenantConstraint, subjectConstraint, forceUpdate);
            _ongoingRequests[key] = task;

            try
            {
                return await task;
            }
            finally
            {
                _ongoingRequests.TryRemove(key, out _);
            }
        }

        private async Task<IEnumerable<TDto>> PerformGetItemsByIds(IEnumerable<Guid> ids, CancellationToken token, Guid? tenantConstraint, Guid? subjectConstraint, bool forceUpdate)
        {
            var itemsToFetch = ids.Where(id => forceUpdate || !_itemList.ContainsKey(id)).ToList();
            if (!itemsToFetch.Any())
            {
                return ids.Select(id => _itemList[id].Item1).Where(item => item != null);
            }

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"api/{EndpointType}/getbyids?1=1");
                if (tenantConstraint.HasValue)
                {
                    sb.Append($"&tenantId={tenantConstraint.Value}");
                }
                if (subjectConstraint.HasValue)
                {
                    sb.Append($"&subjectId={subjectConstraint.Value}");
                }
                var response = await _httpClient.PostAsJsonAsync(sb.ToString(), itemsToFetch, token);
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return Enumerable.Empty<TDto>();
                if (response.IsSuccessStatusCode)
                {
                    var items = await response.Content.ReadFromJsonAsync<IEnumerable<TDto>>(token);
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            _itemList.TryAdd(item.Id, new Tuple<TDto, DateTime>(item, DateTime.UtcNow));
                        }
                        return items;
                    }
                }
                return Enumerable.Empty<TDto>();
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Failed to get items by IDs: {ex.Message}", Severity.Error);
                return Enumerable.Empty<TDto>();
            }
        }

        public async Task<IEnumerable<TDto>> GetAllItems(CancellationToken token, Guid? tenantConstraint, Guid? subjectConstraint)
        {
            var key = $"getall_{tenantConstraint}_{subjectConstraint}";
            
            if (_ongoingRequests.TryGetValue(key, out var existingTask) && existingTask is Task<IEnumerable<TDto>> existingGetAllTask)
            {
                return await existingGetAllTask;
            }

            var task = PerformGetAllItems(token, tenantConstraint, subjectConstraint);
            _ongoingRequests[key] = task;

            try
            {
                return await task;
            }
            finally
            {
                _ongoingRequests.TryRemove(key, out _);
            }
        }

        private async Task<IEnumerable<TDto>> PerformGetAllItems(CancellationToken token, Guid? tenantConstraint, Guid? subjectConstraint)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"api/{EndpointType}/getall");
                if (tenantConstraint.HasValue)
                {
                    sb.Append($"&tenantId={tenantConstraint.Value}");
                }
                if (subjectConstraint.HasValue)
                {
                    sb.Append($"&subjectId={subjectConstraint.Value}");
                }
                var response = await _httpClient.GetAsync(sb.ToString(), token);
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return Enumerable.Empty<TDto>();
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<TDto>>(cancellationToken: token);
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        _itemList.TryAdd(item.Id, new Tuple<TDto, DateTime>(item, DateTime.UtcNow));
                    }
                    return result;
                }
                return Enumerable.Empty<TDto>();
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Failed to get all items: {ex.Message}", Severity.Error);
                return Enumerable.Empty<TDto>();
            }
        }

        public async Task AddItem(TDto item, CancellationToken token)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/{EndpointType}/create", item, token);
                if (response.IsSuccessStatusCode)
                {
                    var createdItemId = await response.Content.ReadFromJsonAsync<Guid>(token);
                    if (createdItemId != Guid.Empty)
                    {
                        item.Id = createdItemId;
                        _itemList[createdItemId] = new Tuple<TDto, DateTime>(item, DateTime.UtcNow);
                    }
                    _snackbar.Add($"Successfully added {EndpointType}", Severity.Success);
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Failed to add item: {ex.Message}", Severity.Error);
            }
        }

        public async Task UpdateItem(TDto item, CancellationToken token)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/{EndpointType}/update", item, token);
                if (response.IsSuccessStatusCode)
                {
                    var updatedItemId = await response.Content.ReadFromJsonAsync<Guid>(token);
                    if (updatedItemId != Guid.Empty)
                    {
                        _itemList[updatedItemId] = new Tuple<TDto, DateTime>(item, DateTime.UtcNow);
                    }
                    _snackbar.Add($"Successfully updated {EndpointType}", Severity.Success);
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Failed to update {EndpointType}: {ex.Message}", Severity.Error);
            }
        }

        public async Task DeleteItem(Guid id, CancellationToken token)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/{EndpointType}/delete/{id}", token);
                if (response.IsSuccessStatusCode)
                {
                    _itemList.Remove(id);
                }
                _snackbar.Add($"Successfully deleted {EndpointType}", Severity.Success);
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Failed to delete {EndpointType}: {ex.Message}", Severity.Error);
            }
        }

        public async Task<IEnumerable<TDto>> GetAllByTenantId(Guid tenantId, CancellationToken token)
        {
            var key = $"getallbytenant_{tenantId}";
            
            if (_ongoingRequests.TryGetValue(key, out var existingTask) && existingTask is Task<IEnumerable<TDto>> existingTenantTask)
            {
                return await existingTenantTask;
            }

            var task = PerformGetAllByTenantId(tenantId, token);
            _ongoingRequests[key] = task;

            try
            {
                return await task;
            }
            finally
            {
                _ongoingRequests.TryRemove(key, out _);
            }
        }

        private async Task<IEnumerable<TDto>> PerformGetAllByTenantId(Guid tenantId, CancellationToken token)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/{EndpointType}/getall?tenantid={tenantId}", token);
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return Enumerable.Empty<TDto>();
                
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<TDto>>(cancellationToken: token);
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        _itemList.TryAdd(item.Id, new Tuple<TDto, DateTime>(item, DateTime.UtcNow));
                    }
                    return result;
                }
                return Enumerable.Empty<TDto>();
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Failed to get items by tenantId: {ex.Message}", Severity.Error);
                return Enumerable.Empty<TDto>();
            }
        }

        public async Task<IEnumerable<TDto>> GetAllBySubjectId(Guid subjectId, CancellationToken token)
        {
            var key = $"getallbysubject_{subjectId}";
            
            if (_ongoingRequests.TryGetValue(key, out var existingTask) && existingTask is Task<IEnumerable<TDto>> existingSubjectTask)
            {
                return await existingSubjectTask;
            }

            var task = PerformGetAllBySubjectId(subjectId, token);
            _ongoingRequests[key] = task;

            try
            {
                return await task;
            }
            finally
            {
                _ongoingRequests.TryRemove(key, out _);
            }
        }

        private async Task<IEnumerable<TDto>> PerformGetAllBySubjectId(Guid subjectId, CancellationToken token)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/{EndpointType}/getall?subjectid={subjectId}", token);
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return Enumerable.Empty<TDto>();
                
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<TDto>>(cancellationToken: token);
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        _itemList.TryAdd(item.Id, new Tuple<TDto, DateTime>(item, DateTime.UtcNow));
                    }
                    return result;
                }
                return Enumerable.Empty<TDto>();
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Failed to get items by subjectId: {ex.Message}", Severity.Error);
                return Enumerable.Empty<TDto>();
            }
        }

        public async Task<IEnumerable<TDto>> GetAllByCustom(string customRoute, CancellationToken token)
        {
            var key = $"getallbycustom_{customRoute}";
            
            if (_ongoingRequests.TryGetValue(key, out var existingTask) && existingTask is Task<IEnumerable<TDto>> existingCustomTask)
            {
                return await existingCustomTask;
            }

            var task = PerformGetAllByCustom(customRoute, token);
            _ongoingRequests[key] = task;

            try
            {
                return await task;
            }
            finally
            {
                _ongoingRequests.TryRemove(key, out _);
            }
        }

        private async Task<IEnumerable<TDto>> PerformGetAllByCustom(string customRoute, CancellationToken token)
        {
            try
            {
                var response = await _httpClient.GetAsync(customRoute, token);
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return Enumerable.Empty<TDto>();
                
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<TDto>>(cancellationToken: token);
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        _itemList.TryAdd(item.Id, new Tuple<TDto, DateTime>(item, DateTime.UtcNow));
                    }
                    return result;
                }
                return Enumerable.Empty<TDto>();
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Failed to get all items: {ex.Message}", Severity.Error);
                return Enumerable.Empty<TDto>();
            }
        }
    }

}

