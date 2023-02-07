using Catalog.API.Repositories;
using Catalog.API.Entities;
using Moq;
using Xunit;
using Catalog.API.Data;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Catalog.API.Test

{
  public class ProductRepoTest
  {
    private Mock<IMongoClient> _mongoClient;
    private Mock<IMongoDatabase> _mongodb;
    private Mock<IMongoCollection<Product>> _productCollection;
    private List<Product> _productList;
    private Mock<IAsyncCursor<Product>> _productCursor;
    private Mock<ICatalogContext> _catalogContext;
    public ProductRepoTest () {
      _productCollection = new Mock<IMongoCollection<Product>>();
      _productCursor = new Mock<IAsyncCursor<Product>>();
      _mongodb = new Mock<IMongoDatabase>();
      _mongoClient = new Mock<IMongoClient>();
      _productList = new List<Product>() {
        new Product()
        {
          Id = "602d2149e773f2a3990b47f5",
          Name = "IPhone X",
          Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
          Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
          ImageFile = "product-1.png",
          Price = 950.00M,
          Category = "Smart Phone"
        },
        new Product()
        {
          Id = "602d2149e773f2a3990b47f6",
          Name = "Samsung 10",
          Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
          Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
          ImageFile = "product-2.png",
          Price = 840.00M,
          Category = "Smart Phone"
        },
      };
      _catalogContext = new Mock<ICatalogContext>();
    }

    private void InitializeMongoDB()
    {
      this._mongodb.Setup(db => db.GetCollection<Product>("Product", default)).Returns(this._productCollection.Object);
      this._mongoClient.Setup(client => client.GetDatabase(It.IsAny<string>(), default)).Returns(this._mongodb.Object);
    }

    private void InitializeMongoProductCollection()
    {
      this._productCursor.Setup( x => x.Current).Returns(this._productList);
      this._productCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);
      this._productCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
        .Returns(Task.FromResult(true)).Returns(Task.FromResult(false));
      this._productCollection
        .Setup( x => x.AggregateAsync(It.IsAny<PipelineDefinition<Product, Product>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(this._productCursor.Object);
      this.InitializeMongoDB();
    }

    [Fact]
    public async Task ItShouldReturnListOfProductsWhenGetProductsIsCalledAsync()
    {
      //this.InitializeMongoProductCollection();
      //var productRepo = new ProductRepository(_catalogContext.Object);
      //Console.WriteLine(await productRepo.GetProducts());
      //Assert.NotEmpty(await productRepo.GetProducts());
    }
  }
}