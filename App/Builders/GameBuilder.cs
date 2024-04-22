using System.Diagnostics;
using GLogger.Api.IgdbApi;
using GLogger.Api.IgdbApi.Endpoints;
using GLogger.Database.MongoDB;
using GLogger.Database.MongoDB.Documents;
using static GLogger.Api.IgdbApi.Endpoints.Game;

namespace GLogger.App.Builders
{
    internal sealed class GameBuilder
    {
        private readonly IgdbClient _apiClient;
        private readonly GameDocumentDispatcher _gameDispatcher;

        //Hardcode the sql statements for now
        private const string query_search_game_by_id = "fields *; where id = {0};";
        private const string query_search_game_by_name = "fields *; where name = \"{0}\"*; limit 25;";

        public GameBuilder(IgdbClient apiClient, MongoDbClient mongoClient)
        {
            _apiClient = apiClient;
            _gameDispatcher = new GameDocumentDispatcher(mongoClient);
        }

        private async Task<GameDocument?> GetGameFromId(int gameId)
        {
            return await _gameDispatcher
                .GetDocumentFromId(gameId, json => new GameDocument(json));
        }
        public async Task<GameDocument?> FetchGameFromid(int gameId, bool useWebData = true)
        {
            var game = await GetGameFromId(gameId);
            if (game == null && useWebData)
            {
                var searchResults = await _apiClient
                    .PostEndpointAsync<List<GameJson>>(Game.Endpoint,
                        string.Format(query_search_game_by_id, gameId));
                if (searchResults == null) 
                { 
                    //TODO: Log error
                    throw new Exception("Could not fetch game from igdb");
                }
                else
                {
                    game = await InsertGame(searchResults?.ResponseData?.FirstOrDefault());
                }
            }

            return game;
        }
        public async Task<List<GameDocument>?> FetchGamesFromName(string gameName, bool useWebData = true)
        {
            if (gameName != null && gameName.Length >= 3 && gameName.Length <= 5)
            {
                useWebData = true;
            }
            var games = await _gameDispatcher.GetGameFromName(gameName ?? "");
            if (games == null && useWebData && gameName != null)
            {
                var searchResults = await _apiClient
                    .PostEndpointAsync<List<GameJson>>(Game.Endpoint,
                        string.Format(query_search_game_by_name, gameName));
                if (searchResults == null) 
                {
                    //TODO: Log error
                    throw new Exception("could not fetch from igdb");
                }
                else
                {
                    if (games == null) games = new List<GameDocument>();
                    foreach(var game in searchResults.ResponseData ?? new List<GameJson>())
                    {
                        var newGame = await InsertGame(game);
                        if (newGame != null) games.Add(newGame);
                    }
                }
            }

            return games;
        }

        private async Task<GameDocument?> InsertGame(GameJson? game)
        {
            if (game == null) return null;

            var games = await _gameDispatcher
                .AddOrUpdateDocument(new List<GameJson> { game }, json => new GameDocument(json));
            return games?.FirstOrDefault();
        }
    }
}