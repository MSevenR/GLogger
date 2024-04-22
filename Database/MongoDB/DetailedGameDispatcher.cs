using GLogger.Api.IgdbApi;
using GLogger.Database.MongoDB.Documents;
using MongoDB.Driver;

namespace GLogger.Database.MongoDB
{
    public class DetailedGameDispatcher
    {
        private readonly IgdbClient _apiClient;
        private readonly IMongoCollection<FullGameDocument> _detailedGameCollection;

        public DetailedGameDispatcher(MongoDbClient mongoClient, IgdbClient apiClient)
        {
            _apiClient = apiClient;
            _detailedGameCollection = mongoClient.GetCollection<FullGameDocument>("detailedgames");
        }

        public async Task<List<FullGameDocument>> GetDetailedGameDataFromName(string gameName)
        {


            return new List<FullGameDocument>();
        }

        public async Task<FullGameDocument?> GetDetailedGameFromId(int gameId)
        {
            return null;
        }
    }
}