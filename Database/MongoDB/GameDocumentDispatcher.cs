using GLogger.Database.MongoDB.Documents;
using MongoDB.Driver;
using static GLogger.Api.IgdbApi.Endpoints.Game;

namespace GLogger.Database.MongoDB
{
    internal sealed class GameDocumentDispatcher
        : DocumentDispatcher<GameDocument, GameJson>
    {
        private const string _collectionName = "games";

        public GameDocumentDispatcher(MongoDbClient mongoClient)
            : base(mongoClient, _collectionName)
        {

        }

        public async Task<List<GameDocument>?> GetGameFromName(string gameName)
        {
            var documentData = await Collection
                .Find(g => g.name != null && g.name.Contains(gameName))
                .ToListAsync();
            if (documentData == null || documentData.Count == 0)
            {
                return null;
            }
            else
            {
                var gameDocuments = new List<GameDocument>();
                foreach(var game in documentData)
                {
                    gameDocuments.Add(new GameDocument(game));
                }
                return gameDocuments;
            }
        }
    }
}