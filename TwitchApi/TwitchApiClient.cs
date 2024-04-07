using System.Text.Json;
using Microsoft.Extensions.Options;
using TwitchApi;

namespace GLogger.TwitchApi
{
    public class TwitchApiClient
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _callUrl;

        public string ClientId { get; set; }

        public TwitchApiClient(IOptions<TwitchApiConfig> config)
        {
            _callUrl = $"{config.Value.BaseUrl}?" +
                       $"client_id={config.Value.ClientId}&" +
                       $"client_secret={config.Value.ClientSecret}&" +
                       $"grant_type={config.Value.GrantType}";

            _httpClient.DefaultRequestHeaders.Add("User-Agent", "mdodds.cloud/testing");
            _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");

            ClientId = config.Value.ClientId;
        }

        public async Task<AccessToken> GetAccessToken()
        {
            var response = await _httpClient.PostAsync(_callUrl, null);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AccessToken>(responseString)
                ?? new AccessToken("invalid", 0, "invalid");
        }
    }
    
}