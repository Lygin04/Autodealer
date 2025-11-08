using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Autodealer.Entities;

public class Engine
{
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("brand"), BsonRepresentation(BsonType.String)]
    public string Brand { get; set; }
    
    [BsonElement("model"), BsonRepresentation(BsonType.String)]
    public string Model { get; set; }
    
    /// <summary>
    /// Объем в литрах.
    /// </summary>
    [BsonElement("capacity"), BsonRepresentation(BsonType.Double)]
    public double Capacity { get; set; }
    
    /// <summary>
    /// Количество цилиндров.
    /// </summary>
    [BsonElement("count_block"), BsonRepresentation(BsonType.Int32)]
    public int CountBlock { get; set; }
}