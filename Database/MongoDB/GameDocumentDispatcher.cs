using GLogger.Api.IgdbApi.Endpoints;
using GLogger.Database.MongoDB.Documents;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GLogger.Database.MongoDB
{
    public class GameDocumentDispatcher
    {
        private readonly IMongoCollection<GameDocument> _gameCollection;
        private readonly IMongoCollection<PlatformDocument> _platformCollection;

        public GameDocumentDispatcher(MongoDbClient mongoClient)
        {
            _gameCollection = mongoClient.GetCollection<GameDocument>("games");
            _platformCollection = mongoClient.GetCollection<PlatformDocument>("platforms");
        }

        public async Task<bool> AddOrUpdateGameDocuments(List<GameJson> games)
        {
            foreach(var game in games)
            {
                var gameDocument = GameJsonToGameDocument(game) ?? null;
                if (gameDocument != null)
                {
                    await _gameCollection.ReplaceOneAsync(
                        g => g.gameid == gameDocument.gameid,
                        gameDocument,
                        new ReplaceOptions { IsUpsert = true }
                    );
                }
            }

            return true;
        }

        public async Task<List<GameDocument>> GetGameDocumentsAsync(string searchData)
        {
            var searchResults = await _gameCollection.FindAsync(searchData);
            return searchResults.ToList();
        }

        public async Task<List<GameDocument>> FindGamesFromName(string gameName)
        {
            var searchResults = await _gameCollection.FindAsync(g => g.name.Contains(gameName));
            return searchResults.ToList();
        }

        private GameDocument? GameJsonToGameDocument(GameJson game, bool update = true)
        {
            if (!update)
            {
                var existingGame = _gameCollection
                    .Find(g => g.gameid == game.id).FirstOrDefault();
                if (existingGame != null) return null;
            }
            return new GameDocument
            {
                gameid = game.id,
                name = game.name ?? string.Empty,
                summary = game.summary ?? string.Empty,
                PlatformDocuments = _platformCollection
                    .Find(p => game.platforms != null && game.platforms.Contains(p.platformid))
                    .ToList()
            };
        }
    }
}