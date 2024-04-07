using Microsoft.Extensions.Options;

namespace TwitchApi
{
    public record TwitchApiConfig(string BaseUrl,
        string ClientId,
        string ClientSecret,
        string GrantType) : IOptions<TwitchApiConfig>
    {
        public TwitchApiConfig Value => this;
    }
}