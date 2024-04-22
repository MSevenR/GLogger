using Microsoft.Extensions.Options;

namespace GLogger.Api.IgdbApi
{
    public record IgdbConfig(string BaseUrl,
        SearchClause SearchClauses) : IOptions<IgdbConfig>
    {
        public IgdbConfig Value => this;
    }
}