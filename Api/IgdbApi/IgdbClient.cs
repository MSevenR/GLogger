using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using GLogger.TwitchApi;
using TwitchApi;

namespace GLogger.Api.IgdbApi
{
    public class IgdbClient
    {
        private readonly HttpClient _httpClient;
        private readonly TwitchApiClient _twitchClient;
        public readonly IgdbConfig Config;
        private AccessToken? _accessToken { get; set; } = null;

        public IgdbClient(TwitchApiClient twitchClient, IgdbConfig config)
        {
            _httpClient = new HttpClient(new HttpClientHandler {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            })
            {
                BaseAddress = new Uri(config.Value.BaseUrl)
            };
            _httpClient.DefaultRequestHeaders.Add("Client-ID", twitchClient.ClientId);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "mdodds.cloud/testing");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            
            _twitchClient = twitchClient;
            Config = config;
        }

        private async Task<AccessToken> GetAccessToken()
        {
            if (_accessToken == null || _accessToken.ExpiresAt < DateTime.Now)
            {
                _accessToken = await _twitchClient.GetAccessToken();
            }

            return _accessToken;
        }

        private async Task<T?> DecompressResponse<T>(HttpResponseMessage? response)
        {
            if (response == null) return default;

            var webResponse = await response.Content.ReadAsStreamAsync();
            if (response.Content.Headers.ContentEncoding.Contains("br"))
            {
                webResponse = new BrotliStream(webResponse, CompressionMode.Decompress);
            }
            else if (response.Content.Headers.ContentEncoding.Contains("zstd"))
            {
                webResponse = new ZstdSharp.DecompressionStream(webResponse);
            }
            else if (response.Content.Headers.ContentEncoding.Contains("gzip"))
            {
                webResponse = new GZipStream(webResponse, CompressionMode.Decompress);
            }

            var responseString = await new StreamReader(webResponse).ReadToEndAsync();
            return JsonSerializer.Deserialize<T>(responseString);
        }

        public async Task<IApiData<T>> PostEndpointAsync<T>(string endpoint, string data)
        {
            var content = new StringContent(data.Replace("\\", ""), Encoding.UTF8, "text/plain");
            var url = $"{ Config.Value.BaseUrl }{ endpoint }";

            var accessToken = await GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.access_token);

            var response = await _httpClient.PostAsync(url, content);
            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                var responseObject = await DecompressResponse<T>(response);
                var errorMessage = responseObject == null ? "No data returned" : "";
                return new ApiData<T> { ResponseData = responseObject, ErrorMessage = errorMessage };
            }
            else
            {
                return new ApiData<T> { ErrorMessage = response.ReasonPhrase ?? "Catastrophic Error" };
            }
        }
    }
}