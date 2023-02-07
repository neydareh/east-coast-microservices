using MongoDB.Driver;
using Catalog.API.Entities;

namespace Catalog.API.Data {
  public class CatalogContext : ICatalogContext {
    private IMongoDatabase _database { get; set; }
    private MongoClient _mongoClient { get; set; }
    public IMongoCollection<Product> Products { get; }

    public CatalogContext(IConfiguration configuration)
    {
      _mongoClient = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
      _database = _mongoClient.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
      Products = _database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
      CatalogContextSeed.SeedData(Products);
    }
  }
}