using GLogger.Api.IgdbApi;
using GLogger.Api.IgdbApi.Endpoints;
using GLogger.Database.MongoDB.Documents;
using MongoDB.Bson;
using MongoDB.Driver;
using static GLogger.Api.IgdbApi.Endpoints.Platform;

namespace GLogger.Database.MongoDB
{
    internal sealed class PlatformDocumentDispatcher 
        : DocumentDispatcher<PlatformDocument, PlatformJson>
    {
        private const string _collectionName = "platforms";
        public PlatformDocumentDispatcher(MongoDbClient mongoClient)
            : base(mongoClient, _collectionName)
        {
            
        }
    }
}