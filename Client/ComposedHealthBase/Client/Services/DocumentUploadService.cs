using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ComposedHealthBase.Shared.DTOs;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;

namespace ComposedHealthBase.BaseClient.Services
{
    public interface IDocumentUploadService
    {
        Task<bool> UploadDocument<TDto>(Tuple<IDocumentDto, IBrowserFile> document, CancellationToken token = default) where TDto : IDocumentDto;
        Task<string> GetSasLink<TDto>(long documentId, CancellationToken token = default) where TDto : IDocumentDto;
    }

    public class DocumentUploadService : IDocumentUploadService
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;

        public DocumentUploadService(HttpClient httpClient, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
        }

        private static string GetEndpointType<TDto>() where TDto : IDocumentDto
        {
            var typeName = typeof(TDto).Name.Replace("Dto", string.Empty).ToLowerInvariant();
            return typeName;
        }

        public async Task<string> GetSasLink<TDto>(long documentId, CancellationToken token = default) where TDto : IDocumentDto
        {
            if (documentId == 0) return string.Empty;

            try
            {
                var endpointType = GetEndpointType<TDto>();
                var response = await _httpClient.GetFromJsonAsync<string>($"api/{endpointType}/getsaslink/{documentId}", token);

                if (!string.IsNullOrEmpty(response))
                {
                    return response.Replace("http://blobstorage", "http://localhost");
                }
                else
                {
                    _snackbar.Add("Failed to get document access link", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Error loading document: {ex.Message}", Severity.Error);
            }
            return string.Empty;
        }

        public async Task<bool> UploadDocument<TDto>(Tuple<IDocumentDto, IBrowserFile> document, CancellationToken token = default) where TDto : IDocumentDto
        {
            try
            {
                var endpointType = GetEndpointType<TDto>();
                using var content = new MultipartFormDataContent();

                // Serialize DTO to JSON and add as a part named "documentDto"
                var dtoJson = JsonSerializer.Serialize(document.Item1);
                content.Add(new StringContent(dtoJson, System.Text.Encoding.UTF8, "application/json"), "documentDto");

                // Add file
                var fileContent = new StreamContent(document.Item2.OpenReadStream(maxAllowedSize: 1024 * 1024 * 10));
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(document.Item2.ContentType);
                content.Add(fileContent, "file", document.Item2.Name);

                var response = await _httpClient.PostAsync($"api/{endpointType}/upload", content, token);

                if (response.IsSuccessStatusCode)
                {
                    _snackbar.Add("Document uploaded successfully.", Severity.Success);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _snackbar.Add($"Failed to upload document: {response.StatusCode} - {errorContent}", Severity.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Error uploading document: {ex.Message}", Severity.Error);
                return false;
            }
        }
    }
}