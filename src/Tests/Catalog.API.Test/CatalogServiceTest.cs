using Catalog.API.Entities;
using Catalog.API.Repositories;
using NSubstitute;

namespace Catalog.API.Test
{
  public class CatalogServiceTest
  {
    private readonly IProductRepository mockedProductRepo;
    public CatalogServiceTest()
    {
      mockedProductRepo = Substitute.For<IProductRepository>();
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
      mockedProductRepo.GetProduct(productId).Returns(mockedProduct);
      // act
      var product = await mockedProductRepo.GetProduct(productId);
      // assert
      Assert.Equal("Product One", product.Name);
      Assert.Equal(productId, product.Id);
    }

    [Fact]
    public async void GetProductById_ShouldReturnNothing_WhenProductDoesntExist()
    {
      // arrange
      mockedProductRepo.GetProduct(Arg.Any<string>()).Returns((Product)null);
      // act
      var product = await mockedProductRepo.GetProduct("productId");
      // assert
      Assert.Null(product);
    }

    [Fact]
    public async void DeleteProduct_ShouldReturnTrue_WhenProductIsDeleted()
    {
      // arrange
      var productId = Guid.NewGuid().ToString();
      var listOfProducts = new List<Product> { new Product { Id = "1", Name = "Product One" }, new Product { Id = "2", Name = "Product Two" } };
      mockedProductRepo.DeleteProduct(productId).Returns(true);

      // act
      var isProductDeleted = await mockedProductRepo.DeleteProduct(productId);

      // assert
      Assert.True(isProductDeleted);
      await mockedProductRepo.Received().DeleteProduct(Arg.Any<string>());
    }
  }
}