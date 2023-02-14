using Catalog.API.Entities;

namespace Catalog.API.Services {

  public interface ICatalogService {
    Task<List<Product>> GetProducts();
    Task<Product> GetProduct(string id);
    Task<List<Product>> GetProductByName(string name);
    Task<List<Product>> GetProductByCategory(string categoryName);
    Task CreateProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string id);
  }
}
