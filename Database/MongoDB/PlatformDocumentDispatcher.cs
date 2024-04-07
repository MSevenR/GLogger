using GLogger.Database.MongoDB.Documents;
using MongoDB.Bson;
using MongoDB.Driver;
using static GLogger.Api.IgdbApi.Endpoints.Platform;

namespace GLogger.Database.MongoDB
{
    public class PlatformDocumentDispatcher
    {
        private readonly IMongoCollection<PlatformDocument> _platformCollection;

        public PlatformDocumentDispatcher(MongoDbClient mongoClient)
        {
            _platformCollection = mongoClient.GetCollection<PlatformDocument>("platforms");
        }

        public async Task<bool> AddOrUpdatePlatformDocument(PlatformJson platform)
        {
            var platformDocument = PlatformJsonToPlatformDocument(platform)
                ?? new PlatformDocument();

            var result = await _platformCollection.ReplaceOneAsync(
                p => p.platformid == platformDocument.platformid,
                platformDocument,
                new ReplaceOptions { IsUpsert = true }
            );

            return result.IsAcknowledged;
        }
        public async Task<bool> AddOrUpdatePlatformDocuments(List<PlatformJson> platforms)
        {
            if (platforms.Count == 0) return true;
            foreach (var platform in platforms)
            {
                var result = await AddOrUpdatePlatformDocument(platform);
                if (!result) return false;
            }

            return true;
        }

        public async Task<List<PlatformDocument>?> AddPlatformsWithoutUpdate(List<PlatformJson> platforms)
        {
            if (platforms.Count == 0) return null;
            var platformDocuments = new List<PlatformDocument>();
            foreach(var platform in platforms)
            {
                var transformResult = PlatformJsonToPlatformDocument(platform, false);
                if (transformResult != null)
                {
                    platformDocuments.Add(transformResult);
                }
            }
            if (platformDocuments.Count > 0)
            {
                await _platformCollection.InsertManyAsync(platformDocuments);
            }
            
            return platformDocuments;
        }
        public async Task<PlatformDocument?> AddPlatformWithoutUpdate(PlatformJson platform)
        {
            var platformDocument = PlatformJsonToPlatformDocument(platform, false);
            if (platformDocument == null) return null;

            await _platformCollection.InsertOneAsync(platformDocument);
            return platformDocument;
        }

        private PlatformDocument? PlatformJsonToPlatformDocument(PlatformJson platform, bool update = true)
        {
            if (!update)
            {
                var existingPlatform = _platformCollection
                    .Find(p => p.platformid == platform.id).FirstOrDefault();
                if (existingPlatform != null) return null;
            }
            return new PlatformDocument
            {
                platformid = platform.id,
                name = platform.name,
                alternative_name = platform.alternative_name,
                checksum = platform.checksum
            };
        }
    }
}