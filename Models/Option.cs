using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace VRBackend.Models;

public class Option
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string Text { get; set; } = null!;
    public bool IsCorrect { get; set; }
}
