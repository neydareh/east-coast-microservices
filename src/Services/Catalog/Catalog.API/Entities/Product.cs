using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.API.Entities
{
  public class Product
  {
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string? Id { get; set; }
    [BsonElement("Name")]
    public string? Name { get; set; }
    [BsonElement("Category")]
    public string? Category { get; set; }
    [BsonElement("Summary")]
    public string? Summary { get; set; }
    [BsonElement("Description")]
    public string? Description { get; set; }
    public string? ImageFile { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public DateTime? UpdateAt { get; set; } = null;
    public DateTime? DeletedAt { get; set; } = null;
  }
}