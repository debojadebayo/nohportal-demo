using System.Net.Http.Json;
using MudBlazor;
using Shared.DTOs.CRM;
using Shared.DTOs.Scheduling;
using ComposedHealthBase.Shared.Interfaces;

namespace ComposedHealthBase.BaseClient.Services;

public interface ILazyLookupService<TDto>
where TDto : ILazyLookup
{
    Dictionary<long, TDto> ItemList { get; set; }
    string ItemToString(long e);
    Task<IEnumerable<long>> ItemSearch(string value, CancellationToken token);
    Task<TDto?> GetItemById(long id, CancellationToken token, bool forceUpdate = false);
    Task<IEnumerable<TDto>> GetItemsByIds(IEnumerable<long> ids, CancellationToken token, bool forceUpdate = false);
    Task<IEnumerable<TDto>> GetAllItems(CancellationToken token);
    Task AddItem(TDto item, CancellationToken token);
    Task UpdateItem(TDto item, CancellationToken token);
    Task DeleteItem(long id, CancellationToken token);
    Task<IEnumerable<TDto>> GetAllByTenantId(long tenantId, CancellationToken token);
    Task<IEnumerable<TDto>> GetAllByTenantIds(IEnumerable<long> tenantIds, CancellationToken token);
    Task<IEnumerable<TDto>> GetAllBySubjectId(long subjectId, CancellationToken token);
    Task<IEnumerable<TDto>> GetAllBySubjectIds(IEnumerable<long> subjectIds, CancellationToken token);
    Task<IEnumerable<TDto>> GetAllByCustom(string customRoute, CancellationToken token);
}

public class LazyLookupService<TDto> : ILazyLookupService<TDto>
where TDto : ILazyLookup
{
    private static string EndpointType => typeof(TDto).Name.Replace("Dto", string.Empty).ToLowerInvariant();
    public Dictionary<long, TDto> ItemList { get; set; } = new();
    private readonly HttpClient _httpClient;
    private readonly ISnackbar _snackbar;
    public LazyLookupService(HttpClient httpClient, ISnackbar snackbar)
    {
        _httpClient = httpClient;
        _snackbar = snackbar;
    }

    public string ItemToString(long e) => e > 0 && ItemList.TryGetValue(e, out TDto? item) ? $"{item.DisplayName}" : string.Empty;
    public async Task<IEnumerable<long>> ItemSearch(string value, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
        {
            return default!;
        }
        try
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<TDto>>($"api/{EndpointType}/search?term=" + value, token);
            if (result == null || ItemList == null)
            {
                return default!;
            }
            foreach (var item in result)
            {
                ItemList.TryAdd(item.Id, item);
            }
            return result?.Select(x => x.Id) ?? Enumerable.Empty<long>();
        }
        catch (Exception ex)
        {
            _snackbar.Add($"Failed to search items: {ex.Message}", Severity.Error);
            return Enumerable.Empty<long>();
        }
    }

    public async Task<TDto?> GetItemById(long id, CancellationToken token, bool forceUpdate = false)
    {
        if (ItemList.TryGetValue(id, out var item) && !forceUpdate)
        {
            return item;
        }

        try
        {
            var result = await _httpClient.GetFromJsonAsync<TDto>($"api/{EndpointType}/getbyid/{id}", token);
            if (result != null)
            {
                ItemList.TryAdd(id, result);
            }
            return result;
        }
        catch (Exception ex)
        {
            _snackbar.Add($"Failed to get item by ID: {ex.Message}", Severity.Error);
            return default;
        }
    }

    public async Task<IEnumerable<TDto>> GetItemsByIds(IEnumerable<long> ids, CancellationToken token, bool forceUpdate = false)
    {
        if (ids == null || !ids.Any())
        {
            return Enumerable.Empty<TDto>();
        }

        var itemsToFetch = ids.Where(id => forceUpdate || !ItemList.ContainsKey(id)).ToList();
        if (!itemsToFetch.Any())
        {
            return ids.Select(id => ItemList[id]).Where(item => item != null);
        }

        try
        {
            var result = await _httpClient.PostAsJsonAsync($"api/{EndpointType}/getbyids", itemsToFetch, token);
            if (result.IsSuccessStatusCode)
            {
                var items = await result.Content.ReadFromJsonAsync<IEnumerable<TDto>>(token);
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        ItemList.TryAdd(item.Id, item);
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

    public async Task<IEnumerable<TDto>> GetAllItems(CancellationToken token)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<TDto>>($"api/{EndpointType}/getall", token);
            if (result != null)
            {
                foreach (var item in result)
                {
                    ItemList.TryAdd(item.Id, item);
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
                var createdItemId = await response.Content.ReadFromJsonAsync<long>(token);
                if (createdItemId > 0)
                {
                    item.Id = createdItemId;
                    ItemList[createdItemId] = item;
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
                var updatedItemId = await response.Content.ReadFromJsonAsync<long>(token);
                if (updatedItemId > 0)
                {
                    ItemList[updatedItemId] = item;
                }
                _snackbar.Add($"Successfully updated {EndpointType}", Severity.Success);
            }
        }
        catch (Exception ex)
        {
            _snackbar.Add($"Failed to update {EndpointType}: {ex.Message}", Severity.Error);
        }
    }

    public async Task DeleteItem(long id, CancellationToken token)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/{EndpointType}/delete/{id}", token);
            if (response.IsSuccessStatusCode)
            {
                ItemList.Remove(id);
            }
            _snackbar.Add($"Successfully deleted {EndpointType}", Severity.Success);
        }
        catch (Exception ex)
        {
            _snackbar.Add($"Failed to delete {EndpointType}: {ex.Message}", Severity.Error);
        }
    }

    public async Task<IEnumerable<TDto>> GetAllByTenantId(long tenantId, CancellationToken token)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<TDto>>($"api/{EndpointType}/getallbytenantid/{tenantId}", token);
            if (result != null)
            {
                foreach (var item in result)
                {
                    ItemList.TryAdd(item.Id, item);
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

    public async Task<IEnumerable<TDto>> GetAllByTenantIds(IEnumerable<long> tenantIds, CancellationToken token)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"api/{EndpointType}/getallbytenantids", tenantIds, token);
            if (response.IsSuccessStatusCode)
            {
                var items = await response.Content.ReadFromJsonAsync<IEnumerable<TDto>>(token);
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        ItemList.TryAdd(item.Id, item);
                    }
                    return items;
                }
            }
            return Enumerable.Empty<TDto>();
        }
        catch (Exception ex)
        {
            _snackbar.Add($"Failed to get items by tenantIds: {ex.Message}", Severity.Error);
            return Enumerable.Empty<TDto>();
        }
    }

    public async Task<IEnumerable<TDto>> GetAllBySubjectId(long subjectId, CancellationToken token)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<TDto>>($"api/{EndpointType}/getallbysubjectid/{subjectId}", token);
            if (result != null)
            {
                foreach (var item in result)
                {
                    ItemList.TryAdd(item.Id, item);
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

    public async Task<IEnumerable<TDto>> GetAllBySubjectIds(IEnumerable<long> subjectIds, CancellationToken token)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"api/{EndpointType}/getallbysubjectids", subjectIds, token);
            if (response.IsSuccessStatusCode)
            {
                var items = await response.Content.ReadFromJsonAsync<IEnumerable<TDto>>(token);
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        ItemList.TryAdd(item.Id, item);
                    }
                    return items;
                }
            }
            return Enumerable.Empty<TDto>();
        }
        catch (Exception ex)
        {
            _snackbar.Add($"Failed to get items by subjectIds: {ex.Message}", Severity.Error);
            return Enumerable.Empty<TDto>();
        }
    }

    public async Task<IEnumerable<TDto>> GetAllByCustom(string customRoute, CancellationToken token)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<TDto>>(customRoute, token);
            if (result != null)
            {
                foreach (var item in result)
                {
                    ItemList.TryAdd(item.Id, item);
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
