using Catalog.API.Entities;
using Catalog.API.Repositories;

namespace Catalog.API.Services {
  public class CatalogService : ICatalogService {
    private IProductRepository _repository;

    public CatalogService(IProductRepository repository)
    {
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));  
    }
    public async Task CreateProduct(Product product)
    {
      await _repository.CreateProduct(product);
    }

    public async Task<bool> DeleteProduct(string id)
    {
      return await _repository.DeleteProduct(id);
    }

    public async Task<Product> GetProduct(string id)
    {
      return await _repository.GetProduct(id);
    }

    public async Task<List<Product>> GetProductByCategory(string categoryName)
    {
      var enumerableList = await _repository.GetProductByCategory(categoryName);
      List<Product> products = enumerableList.ToList();
      return products;
    }

    public async Task<List<Product>> GetProductByName(string name)
    {
      var enumerableList = await _repository.GetProductByName(name);
      return enumerableList.ToList();
    }

    public async Task<List<Product>> GetProducts()
    {
      var enumerableList = await _repository.GetProducts();
      return enumerableList.ToList();
    }

    public async Task<bool> UpdateProduct(Product product)
    {
      return await _repository.UpdateProduct(product);
    }
  }
}
