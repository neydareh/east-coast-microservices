using Catalog.API.Data;
using MongoDB.Driver;
using Catalog.API.Entities;
using System.Globalization;

namespace Catalog.API.Repositories
{
  public class ProductRepository : IProductRepository
  {
    private readonly ICatalogContext _context;
    private readonly ILogger _logger;
    private TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

    public ProductRepository(ICatalogContext context, ILogger logger)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
      var products = await _context.Products.Find(product => true).ToListAsync();
      _logger.LogInformation($"The current number of products in the inventory is: {products.Count}");
      return products;
    }
    public async Task<Product> GetProduct(string id)
    {
      var product = await _context.Products.Find(product => product.Id == id).FirstOrDefaultAsync();
      _logger.LogInformation($"{product.ToString()} was successfully found");
      return product;
    }
    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
      FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(product => product.Name, textInfo.ToLower(name));
      var filteredList = await _context.Products.Find(filter).ToListAsync();
      if (filteredList.Count > 0)
      {
        _logger.LogInformation($"Product with name: {name} was found");
        return filteredList;
      }
      _logger.LogInformation($"Product with name: {name} was not found");
      return filteredList;
    }
    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
    {
      FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(product => product.Category, textInfo.ToLower(categoryName));
      return await _context.Products.Find(filter).ToListAsync();
    }
    public async Task CreateProduct(ProductDto productRequest)
    {
      if (productRequest != null)
      {
        var product = new Product
        {
          Id = Guid.NewGuid().ToString(),
          Name = textInfo.ToLower(productRequest.Name!),
          Category = textInfo.ToLower(productRequest.Category!),
          Summary = productRequest.Summary,
          Description = productRequest.Description,
          ImageFile = productRequest.ImageFile,
          Price = productRequest.Price
        };
        _logger.LogInformation($"Product: {product.ToString()} was successfully created");
        await _context.Products.InsertOneAsync(product);
      }
    }
    public async Task<bool> UpdateProduct(ProductDto productRequest)
    {
      if (productRequest != null)
      {
        var product = new Product
        {
          Id = Guid.NewGuid().ToString(),
          Name = textInfo.ToLower(productRequest.Name!),
          Category = textInfo.ToLower(productRequest.Category!),
          Summary = productRequest.Summary,
          Description = productRequest.Description,
          ImageFile = productRequest.ImageFile,
          Price = productRequest.Price,
          UpdateAt = DateTime.UtcNow,
        };
        var updatedResult = await _context.Products.ReplaceOneAsync(filter: oldProduct => oldProduct.Id == product.Id, replacement: product);
        var isUpdated = updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        if (isUpdated)
        {
          _logger.LogInformation($"Product with Id: {product.Id} was successfully updated");
          return isUpdated;
        }
      }
      return false;
    }
    public async Task<bool> DeleteProduct(string id)
    {
      FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(product => product.Id, id);
      DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);
      var isDeleted = deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
      if (isDeleted)
      {
        _logger.LogInformation($"Product with Id: {id} was successfully deleted");
        return isDeleted;
      }
      _logger.LogInformation($"Failed to delete product with Id: {id}");
      return false;
    }
  }
}
