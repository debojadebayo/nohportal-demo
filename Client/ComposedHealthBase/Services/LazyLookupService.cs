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
}

public class LazyLookupService<TDto> : ILazyLookupService<TDto>
where TDto : ILazyLookup
{
    public Dictionary<long, TDto> ItemList { get; set; } = new();
    private readonly HttpClient _httpClient;
    public LazyLookupService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public string ItemToString(long e) => e > 0 && ItemList.TryGetValue(e, out TDto? item) ? $"{item.DisplayName} - {item.Id}" : string.Empty;
    public async Task<IEnumerable<long>> ItemSearch(string value, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
        {
            return default!;
        }
        try
        {
            var endpointType = typeof(TDto).Name.Replace("Dto", string.Empty).ToLowerInvariant();
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<TDto>>($"api/{endpointType}/search?term=" + value, token);
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
            //Snackbar.Add($"Failed to search items: {ex.Message}", Severity.Error);
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
            var endpointType = typeof(TDto).Name.Replace("Dto", string.Empty).ToLowerInvariant();
            var result = await _httpClient.GetFromJsonAsync<TDto>($"api/{endpointType}/getbyid/{id}", token);
            if (result != null)
            {
                ItemList.TryAdd(id, result);
            }
            return result;
        }
        catch (Exception ex)
        {
            //Snackbar.Add($"Failed to get item by ID: {ex.Message}", Severity.Error);
            return default;
        }
    }
}
