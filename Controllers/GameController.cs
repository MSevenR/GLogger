using GLogger.Api;
using GLogger.Api.IgdbApi;
using GLogger.Api.IgdbApi.Endpoints;
using GLogger.Database.MongoDB;
using GLogger.Database.MongoDB.Documents;
using Microsoft.AspNetCore.SignalR.Protocol;
using MongoDB.Driver;
using static GLogger.Api.IgdbApi.Endpoints.Platform;

namespace GLogger.Controllers
{
    public class GameController
    {
        private readonly IgdbClient _apiClient;

        private readonly GameDocumentDispatcher _gameDispatcher;
        private readonly PlatformDocumentDispatcher _platformDispatcher;

        private readonly MongoDbClient _mongoClient;

        public GameController(IgdbClient apiClient, MongoDbClient mongoClient)
        {
            _apiClient = apiClient;
            _gameDispatcher = new GameDocumentDispatcher(mongoClient);
            _platformDispatcher = new PlatformDocumentDispatcher(mongoClient);

            _mongoClient = mongoClient;
        }

        public async Task<IApiData<List<GameDocument>>> GameSummarySearchByName(string gameName, bool useWebApi = false)
        {   
            var result = new ApiData<List<GameDocument>>();
            //Fetch web data
            var searchData = $"fields id, name, summary, platforms; where name = \"{ gameName }\"*; limit 100;";
            if (!await FetchGameDataFromWeb(searchData))
            {
                return new ApiData<List<GameDocument>> {
                    ResponseData = new List<GameDocument>(),
                    ErrorMessage = "Could not fetch from igdb"
                };
            }

            //Fetch local data
            result.ResponseData = await _gameDispatcher.FindGamesFromName(gameName);

            return result;
        }

        private async Task<bool> FetchGameDataFromWeb(string searchData)
        {
            var searchResults = await _apiClient
                .PostEndpointAsync<List<GameJson>>(Game.Endpoint, searchData);
            if (searchResults?.ResponseData?.Count > 0)
            {
                var responseData = searchResults.ResponseData ?? new List<GameJson>();
                var platformIds = responseData.SelectMany(x => x.platforms ?? []).Distinct().ToList() ??
                    new List<int>();
                if (!await AddOrUpdatePlatformData(platformIds))
                {
                    Console.WriteLine("Failed to add or update platform documents.");
                    return false;
                }

                var result = await _gameDispatcher.AddOrUpdateGameDocuments(responseData);

                return true;
            }
            else
            {
                Console.WriteLine("No games found.");
                return false;
            }
        }

        private async Task<bool> AddOrUpdatePlatformData(List<int> platformIds)
        {
            var searchData = $"fields id, name, alternative_name, checksum; where id = ({ string.Join(",", platformIds) }); limit 50;";
            return await FetchPlatformData(searchData);
        }
        private async Task<bool> FetchPlatformData(string searchData)
        {
            var searchResults = await _apiClient
                .PostEndpointAsync<List<PlatformJson>>(Platform.Endpoint, searchData);
            if (searchResults?.ResponseData?.Count > 0)
            {
                var responseData = searchResults.ResponseData ?? new List<PlatformJson>();
                await _platformDispatcher.AddPlatformsWithoutUpdate(responseData);
                return true;
            }
            else
            {
                Console.WriteLine("No platforms found.");
                return false;
            }
        }
    }
}