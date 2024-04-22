using MongoDB.Driver;

namespace GLogger.Database.MongoDB
{
    internal class DocumentDispatcher<TDocument, TJson> where TJson : BaseRecord
    {
        private readonly IMongoCollection<TJson> _collection;
        protected IMongoCollection<TJson> Collection => _collection;

        public DocumentDispatcher(MongoDbClient mongoClient, string collectionName)
        {
            _collection = mongoClient.GetCollection<TJson>(collectionName);
        }

        public async Task<TDocument?> GetDocumentFromId(int id, Func<TJson, TDocument> converter)
        {
            var jsonData = await _collection
                .Find(g => g.id == id)
                .FirstOrDefaultAsync();
            if (jsonData == null)
                return default;
            else
                return converter(jsonData);
        }

        public async Task<List<TDocument>?> AddOrUpdateDocument(List<TJson>? jsonObjects, Func<TJson, TDocument> converter)
        {
            if (jsonObjects == null) return null;

            var documentData = new List<TDocument>();
            foreach(var jsonObject in jsonObjects)
            {
                await _collection.ReplaceOneAsync(
                    p => p.id == jsonObject.id,
                    jsonObject,
                    new ReplaceOptions { IsUpsert = true });
                documentData.Add(converter(jsonObject));
            }

            return documentData;
        }
    }
}