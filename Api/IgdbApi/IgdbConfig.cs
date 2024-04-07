using Microsoft.Extensions.Options;

namespace GLogger.Api.IgdbApi
{
    public record IgdbConfig(string BaseUrl) : IOptions<IgdbConfig>
    {
        public IgdbConfig Value => this;
    }
}