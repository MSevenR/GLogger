using GLogger.Api.IgdbApi;
using GLogger.Api.IgdbApi.Endpoints;
using GLogger.Database.MongoDB;
using GLogger.Database.MongoDB.Documents;
using static GLogger.Api.IgdbApi.Endpoints.Platform;

namespace GLogger.App.Builders
{
    internal sealed class PlatformBuilder
    {
        private readonly IgdbClient _apiClient;
        private readonly PlatformDocumentDispatcher _platformDispatcher;

        //Hardcode the sql statements for now
        private const string query_search_platform_by_id = "fields *; where id = {0};";

        public PlatformBuilder(IgdbClient apiClient, MongoDbClient mongoClient)
        {
            _apiClient = apiClient;
            _platformDispatcher = new PlatformDocumentDispatcher(mongoClient);
        }

        public async Task<PlatformDocument?> FetchPlatformFromId(int platformId, bool useWebData = true)
        {
            var platform = await _platformDispatcher
                .GetDocumentFromId(platformId, json => new PlatformDocument(json));
            if (platform == null && useWebData)
            {
                var searchResults = await _apiClient
                    .PostEndpointAsync<List<PlatformJson>>(Platform.Endpoint,
                        string.Format(query_search_platform_by_id, platformId));
                if (searchResults == null)
                {
                    //TODO: Log error
                    throw new Exception("Could not fetch platform from igdb");
                }
                else
                {
                    platform = await InsertPlatform(searchResults?.ResponseData?.FirstOrDefault());
                }
            }
            return platform;
        }

        private async Task<PlatformDocument?> InsertPlatform(PlatformJson? platformJson)
        {
            if (platformJson == null) return null;

            var platforms = await _platformDispatcher
                .AddOrUpdateDocument(new List<PlatformJson> { platformJson }, json => new PlatformDocument(json));
            return platforms?.FirstOrDefault();
        }
    }
}