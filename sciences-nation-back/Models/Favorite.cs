using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace sciences_nation_back.Models
{
    [BsonIgnoreExtraElements]
    public class Favorite
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("productId")]
        public string? ProductId { get; set; }
    }
}