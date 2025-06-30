using Microsoft.AspNetCore.Authentication;
using RetroOfTheWeek.Models;
using RetroOfTheWeekFrontEnd.Controllers;
using RetroOfTheWeekShared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;

namespace RetroOfTheWeekFrontEnd.API
{
    public class RetroOfTheWeekApiService
    {
        protected readonly HttpClient _httpClient;
        private readonly ILogger<RetroOfTheWeekApiService> _logger;
        private readonly IConfiguration _configuration;

        public RetroOfTheWeekApiService(HttpClient httpClient, ILogger<RetroOfTheWeekApiService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;

            _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("BackendApiUrl") ?? string.Empty);
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new ArgumentException("Endpoint cannot be null or empty", nameof(endpoint));
            }

            return await _httpClient.GetFromJsonAsync<T>(endpoint);
        }

        public async Task<T?> PostAsync<T>(string endpoint, object data)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            throw new HttpRequestException($"Failed to make call to {endpoint}: {response.ReasonPhrase}");
        }


        #region Authentication

        // Most API calls will require an authentication token, so this was put in the parent class
        public async Task<TokenResultModel?> GetSecurityToken(TokenRequestModel tokenRequest)
        {
            try
            {
                return await PostAsync<TokenResultModel>(EndpointPathConstants.Auth.RequestToken, tokenRequest);
            }
            catch(HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception($"Error requesting token: {ex.Message}");
            }
        }
        #endregion
    }
}
