using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GLogger.Database.MongoDB.Documents
{
    public class GameDocument
    {
        [BsonId]
        [BsonElement("id")]
        public int gameid { get; set; }

        public string? name { get; set; } = string.Empty;
        public string? summary { get; set; } = string.Empty;
        public List<PlatformDocument>? PlatformDocuments { get; set; } = new List<PlatformDocument>();
    }
}