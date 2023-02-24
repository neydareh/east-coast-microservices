using MongoDB.Driver;
using Catalog.API.Entities;

namespace Catalog.API.Data {
  public class CatalogContextSeed {
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
      bool doesProductExist = productCollection.Find(product => true).Any();
      if (!doesProductExist)
      {
        productCollection.InsertManyAsync(GetPreconfiguredProducts());
      }
    }

    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
      return new List<Product>()
      {
        new Product()
        {
          Id = "602d2149e773f2a3990b47f5",
          Name = "iphone x",
          Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
          Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
          ImageFile = "product-1.png",
          Price = 950.00M,
          Category = "smartphone"
        },
        new Product()
        {
          Id = "602d2149e773f2a3990b47f6",
          Name = "samsung 10",
          Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
          Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
          ImageFile = "product-2.png",
          Price = 840.00M,
          Category = "smartphone"
        },
        new Product()
        {
          Id = "602d2149e773f2a3990b47f7",
          Name = "huawei plus",
          Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
          Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
          ImageFile = "product-3.png",
          Price = 650.00M,
          Category = "appliances"
        },
        new Product()
        {
          Id = "602d2149e773f2a3990b47f8",
          Name = "xiaomi mi 9",
          Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
          Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
          ImageFile = "product-4.png",
          Price = 470.00M,
          Category = "appliances"
        },
        new Product()
        {
          Id = "602d2149e773f2a3990b47f9",
          Name = "htc u11 plus",
          Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
          Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
          ImageFile = "product-5.png",
          Price = 380.00M,
          Category = "smartphone"
        },
        new Product()
        {
          Id = "602d2149e773f2a3990b47fa",
          Name = "lg g7",
          Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
          Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
          ImageFile = "product-6.png",
          Price = 240.00M,
          Category = "smartphone"
        }
      };
    }
  }
}
