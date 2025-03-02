using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace VRBackend.Models
{
    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string Text { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public List<Option> Options { get; set; } = null!;
    }
}
