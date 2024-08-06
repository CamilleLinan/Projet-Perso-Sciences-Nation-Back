using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace sciences_nation_back.Models
{
    [BsonIgnoreExtraElements]
    public class Product
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("name")]
        public required string Name { get; set; }

        [BsonElement("price")]
        public required string Price { get; set; }

        [BsonElement("img")]
        public required byte[] Img { get; set; }
	}
}