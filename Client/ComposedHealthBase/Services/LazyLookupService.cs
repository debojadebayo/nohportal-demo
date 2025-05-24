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
    string ItemToString(TDto? e);
    Task<IEnumerable<TDto>> ItemSearch(string value, CancellationToken token);
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

    public string ItemToString(TDto? e) => e is null ? string.Empty : $"{e.DisplayName} - {e.Id}";
    public async Task<IEnumerable<TDto>> ItemSearch(string value, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return ItemList.Values;
        }
        try
        {
            var endpointType = typeof(TDto).Name.Replace("Dto", string.Empty).ToLowerInvariant();
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<TDto>>($"api/{endpointType}/search?term=" + value, token);
            if (result == null || ItemList == null)
            {
                return Enumerable.Empty<TDto>();
            }
            foreach (var item in result)
            {
                ItemList.TryAdd(item.Id, item);
            }
            return result;
        }
        catch (Exception ex)
        {
            //Snackbar.Add($"Failed to search items: {ex.Message}", Severity.Error);
            return Enumerable.Empty<TDto>();
        }
    }
}
