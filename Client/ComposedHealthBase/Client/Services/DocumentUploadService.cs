using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ComposedHealthBase.Shared.DTOs;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Shared.DTOs.CRM;
using System.Net.Http.Json;

namespace ComposedHealthBase.BaseClient.Services
{
    public interface IDocumentUploadService
    {
        Task<bool> UploadDocument(Tuple<IDocumentDto, IBrowserFile> document, long? tenantId = null, CancellationToken token = default);
        Task<string> GetSasLink(long documentId, CancellationToken token = default);
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

        public async Task<string> GetSasLink(long documentId, CancellationToken token = default)
        {
            if (documentId == 0) return string.Empty;

            try
            {
                var response = await _httpClient.GetFromJsonAsync<string>($"api/document/getsaslink/{documentId}");

                if (response != null && !string.IsNullOrEmpty(response))
                {
                    return response.Replace("http://blobstorage", "http://localhost");
                }
                else
                {
                    _snackbar.Add("Failed to get document access link", MudBlazor.Severity.Error);
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Error loading document: {ex.Message}", MudBlazor.Severity.Error);
            }
            return string.Empty;
        }

        public async Task<bool> UploadDocument(Tuple<IDocumentDto, IBrowserFile> document, long? tenantId = null, CancellationToken token = default)
        {
            try
            {
                using var content = new MultipartFormDataContent();
                var dto = document.Item1;
                var file = document.Item2;

                content.Add(new StringContent(dto.Name ?? string.Empty), "Name");
                content.Add(new StringContent(dto.Description ?? string.Empty), "Description");
                content.Add(new StringContent(dto.FilePath ?? string.Empty), "FilePath");
                content.Add(new StringContent((tenantId ?? dto.TenantId).ToString()), "TenantId");

                var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 10));
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "file", file.Name);

                var response = await _httpClient.PostAsync("api/document/upload", content, token);

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
