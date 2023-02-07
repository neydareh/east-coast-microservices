using System;
using Catalog.API.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using Catalog.API.Entities;


namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
      private readonly ICatalogContext _context;

      public ProductRepository(ICatalogContext context)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
      }

      public async Task<IEnumerable<Product>> GetProducts()
      {
        return await _context.Products.Find(product => true).ToListAsync();
      }

      public async Task<Product> GetProduct(string id)
      {
        return await _context.Products.Find(product => product.Id == id).FirstOrDefaultAsync();
      }

      public async Task<IEnumerable<Product>> GetProductByName(string name)
      {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(product => product.Name, name);
        return await _context.Products.Find(filter).ToListAsync();
      }
      public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
      {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(product => product.Category, categoryName);
        return await _context.Products.Find(filter).ToListAsync();
      }
      public async Task CreateProduct (Product product)
      {
        await _context.Products.InsertOneAsync(product);
      }
      public async Task<bool> UpdateProduct (Product product)
      {
        var updatedResult = await _context.Products.ReplaceOneAsync(filter: oldProduct => oldProduct.Id == product.Id, replacement: product);

        return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
      }
      public async Task<bool> DeleteProduct (string id)
      {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(product => product.Id, id);
        DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
      }
    }
}
