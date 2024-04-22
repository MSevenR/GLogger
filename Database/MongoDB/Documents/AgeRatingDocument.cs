using GLogger.Api.IgdbApi.Endpoints;
using MongoDB.Bson.Serialization.Attributes;

namespace GLogger.Database.MongoDB.Documents
{
    public class AgeRatingDocument
    {
        [BsonId]
        [BsonElement("id")]
        public int ageratingid { get; set; }

        public AgeRating.Category category { get; set; }
        public Guid checksum { get; set; }
    }
}