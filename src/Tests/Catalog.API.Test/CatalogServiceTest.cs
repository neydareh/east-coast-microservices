using Catalog.API.Entities;
using Catalog.API.Repositories;
using Catalog.API.Services;
using Moq;

namespace Catalog.API.Test {
  public class CatalogServiceTest {

    private readonly CatalogService _catalogService;
    private Mock<IProductRepository> _productRepositoryMock = new();

    public CatalogServiceTest()
    {
      _catalogService = new CatalogService(_productRepositoryMock.Object);
    }

    [Fact]
    public async void GetProductById_ShouldReturnProduct_WhenProductExists()
    {
      // arrange
      var productId = Guid.NewGuid().ToString();
      var mockedProduct = new Product
      {
        Id = productId,
        Name = "Product One"
      };
      _productRepositoryMock.Setup(x => x.GetProduct(productId)).ReturnsAsync(mockedProduct);

      // act
      var product = await _catalogService.GetProduct(productId);

      // assert
      Assert.Equal(productId, product.Id);
      Assert.Equal("Product One", product.Name);
    }

    [Fact]
    public async void GetProductById_ShouldReturnNothing_WhenProductDoesntExist()
    {
      // arrange
      _productRepositoryMock.Setup(x => x.GetProduct(It.IsAny<string>())).ReturnsAsync(() => null);

      // act
      var product = await _catalogService.GetProduct("1");

      // assert
      Assert.Null(product);
    }

    [Fact]
    public async void DeleteProduct_ShouldReturnTrue_WhenProductIsDeleted()
    {
      // arrange
      var productId = Guid.NewGuid().ToString();
      var listOfProducts = new List<Product> { new Product { Id = "1", Name = "Product One" }, new Product { Id = "2", Name = "Product Two" } };
      _productRepositoryMock.Setup(x => x.DeleteProduct(It.IsAny<string>())).ReturnsAsync(true);

      // act
      var isProductDeleted = await _catalogService.DeleteProduct(productId);

      // assert
      Assert.True(isProductDeleted);
      _productRepositoryMock.Verify(x => x.DeleteProduct(productId), Times.Once());
    }
  }
}