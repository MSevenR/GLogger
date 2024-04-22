using GLogger.Database.MongoDB.Documents.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static GLogger.Api.IgdbApi.Endpoints.Platform;

namespace GLogger.Database.MongoDB.Documents
{
    public class PlatformDocument : IPlatformSummary
    {
        private readonly PlatformJson _platformJson;

        public int platformid => _platformJson.id;
        public string Name => _platformJson.name ?? "Unknown";
        public string AlternativeName => _platformJson.alternative_name ?? "";
        public Guid Checksum => _platformJson.checksum ?? Guid.Empty;
        public DateTime CreatedAt => _platformJson.created_at ?? DateTime.MinValue;
        public int Generation => _platformJson.generation ?? -1;
        public int PlatformFamily => _platformJson.platform_family ?? -1;
        public int PlatformLogo => _platformJson.platform_logo ?? -1;
        public string Slug => _platformJson.slug ?? "";
        public string Summary => _platformJson.summary ?? "";
        public DateTime UpdatedAt => _platformJson.updated_at ?? DateTime.MinValue;
        public string Url => _platformJson.url ?? "";
        public int[] Websites => _platformJson.websites ?? [];
        public int[] Versions => _platformJson.versions ?? [];

        public PlatformDocument(PlatformJson platformJson)
        {
            _platformJson = platformJson;
        }
    }
}