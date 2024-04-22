using Microsoft.Extensions.Options;

namespace GLogger.Api.IgdbApi
{
    public record SearchClause(string GameSummarySearchByName,
        string GameSearchById,
        string PlatformSummarySearchById) : IOptions<SearchClause>
    {
        public SearchClause Value => this;
    }
}