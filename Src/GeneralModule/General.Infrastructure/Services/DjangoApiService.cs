using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;


namespace NGErp.General.Infrastructure.Services
{
    public class  DjangoApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DjangoApiService> _logger;

        public DjangoApiService(HttpClient httpClient, ILogger<DjangoApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<T?> GetAsync<T>(string endpoint, string? bearerToken = null)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                
                if (!string.IsNullOrEmpty(bearerToken))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
                }

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Django API GET {Endpoint}", endpoint);
                throw;
            }
        }

        public async Task<T?> PostAsync<T>(string endpoint, object data, string? bearerToken = null)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
                {
                    Content = JsonContent.Create(data)
                };

                if (!string.IsNullOrEmpty(bearerToken))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
                }

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Django API POST {Endpoint}", endpoint);
                throw;
            }
        }

        public async Task<T?> PatchAsync<T>(string endpoint, object data, string? bearerToken = null)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Patch, endpoint)
                {
                    Content = JsonContent.Create(data)
                };

                if (!string.IsNullOrEmpty(bearerToken))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
                }

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Django API PATCH {Endpoint}", endpoint);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string endpoint, string? bearerToken = null)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);

                if (!string.IsNullOrEmpty(bearerToken))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
                }

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Django API DELETE {Endpoint}", endpoint);
                throw;
            }
        }
    }
}
