using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GLogger.Database.MongoDB.Documents
{
    public class PlatformDocument
    {
        [BsonId]
        [BsonElement("id")]
        public int platformid { get; set; }

        public string name { get; set; } = string.Empty;
        public string alternative_name { get; set; } = string.Empty;
        public string checksum { get; set; } = string.Empty;
    }
}