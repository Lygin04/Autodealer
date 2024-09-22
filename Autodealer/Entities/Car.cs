using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Autodealer.Model;

public class Car
{
    /// <summary>
    /// Индификатор.
    /// </summary>
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    /// <summary>
    /// Марка.
    /// </summary>
    [BsonElement("brand"), BsonRepresentation(BsonType.String)]
    public string Brand { get; set; }
    /// <summary>
    /// Модель.
    /// </summary>
    [BsonElement("model"), BsonRepresentation(BsonType.String)]
    public string Model { get; set; }
    /// <summary>
    /// Поколение.
    /// </summary>
    [BsonElement("generation"), BsonRepresentation(BsonType.String)]
    public string Generation { get; set; }
    /// <summary>
    /// Двигатель.
    /// </summary>
    [BsonElement("engine"), BsonRepresentation(BsonType.String)]
    public string Engine { get; set; }
}