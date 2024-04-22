using GLogger.Api.IgdbApi;
using GLogger.App.Builders;
using GLogger.Database.MongoDB;

namespace GLogger.Controllers
{
    internal sealed class PlatformController
    {
        private readonly PlatformBuilder _platformBuilder;

        public PlatformController(IgdbClient client, MongoDbClient mongoClient)
        {
            _platformBuilder = new PlatformBuilder(client, mongoClient);
        }

        
    }
}