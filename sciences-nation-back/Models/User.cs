using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace sciences_nation_back.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("firstname")]
        public required string FirstName { get; set; }

        [BsonElement("lastname")]
        public required string LastName { get; set; }

        [BsonElement("email")]
        public required string Email { get; set; }

        [BsonElement("password")]
        public required string Password { get; set; }

        [BsonElement("favorites")]
        public List<string> Favorites { get; set; } = new List<string>();
    }
}