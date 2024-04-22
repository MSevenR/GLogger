using GLogger.Api;
using GLogger.Api.IgdbApi;
using GLogger.App.Builders;
using GLogger.Database.MongoDB;
using GLogger.Database.MongoDB.Documents;

namespace GLogger.Controllers
{
    public sealed class GameController
    {
        private readonly GameBuilder _gameBuilder;

        public GameController(IgdbClient apiClient, MongoDbClient mongoClient)
        {
            _gameBuilder = new GameBuilder(apiClient, mongoClient);
        }

        public async Task<IApiData<GameDocument>> GetGame(int gameId)
        {
            var game = await _gameBuilder.FetchGameFromid(gameId);
            if (game == null)
            {
                return new ApiData<GameDocument> {
                    ResponseData = null,
                    ErrorMessage = "Game not found"
                };
            }
            else
            {
                return new ApiData<GameDocument> {
                    ResponseData = game,
                    ErrorMessage = ""
                };
            }
        }

        public async Task<IApiData<List<GameDocument>>> GetGamesFromName(string gameName)
        {
            if (gameName == null || gameName.Length < 4)
            {
                return new ApiData<List<GameDocument>> {
                    ResponseData = new List<GameDocument>(),
                    ErrorMessage = "Game name must be at least 4 characters long"
                };
            }

            var games = await _gameBuilder.FetchGamesFromName(gameName);
            if (games == null)
            {
                return new ApiData<List<GameDocument>> {
                    ResponseData = new List<GameDocument>(),
                    ErrorMessage = "No games found"
                };
            }
            else
            {
                return new ApiData<List<GameDocument>> {
                    ResponseData = games,
                    ErrorMessage = ""
                };
            }
        }
    }
}