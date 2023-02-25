using MongoDB.Driver;
using Catalog.API.Entities;

namespace Catalog.API.Data
{
  public class CatalogContextSeed
  {
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
          Id = "c0b526ea-84f6-49eb-9530-cf83da4d63a2",
          Name = "iphone x",
          Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
          Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
          ImageFile = "product-1.png",
          Price = 950.00M,
          Category = "smartphone",
          DeletedAt = null,
          UpdateAt = null,
        },
        new Product()
        {
          Id = "0a5cdc83-fde8-4377-a349-54864fb30c40",
          Name = "samsung 10",
          Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
          Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
          ImageFile = "product-2.png",
          Price = 840.00M,
          Category = "smartphone",
          DeletedAt = null,
          UpdateAt = null,
        },
        new Product()
        {
          Id = "0a5cdc83-fde8-4377-a349-54864fb30c49",
          Name = "huawei plus",
          Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
          Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
          ImageFile = "product-3.png",
          Price = 650.00M,
          Category = "appliances",
          DeletedAt = null,
          UpdateAt = null,
        },
        new Product()
        {
          Id = "b1e2e94c-5185-4494-ba18-8ce57c55128b",
          Name = "xiaomi mi 9",
          Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
          Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
          ImageFile = "product-4.png",
          Price = 470.00M,
          Category = "appliances",
          DeletedAt = null,
          UpdateAt = null,
        },
        new Product()
        {
          Id = "24b912ad-aa33-42f5-a8eb-07bca9ab7041",
          Name = "htc u11 plus",
          Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
          Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
          ImageFile = "product-5.png",
          Price = 380.00M,
          Category = "smartphone",
          DeletedAt = null,
          UpdateAt = null,
        },
        new Product()
        {
          Id = "17e6a353-aa23-40dc-9a08-1a8dca3ba989",
          Name = "lg g7",
          Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
          Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
          ImageFile = "product-6.png",
          Price = 240.00M,
          Category = "smartphone",
          DeletedAt = null,
          UpdateAt = null,
        }
      };
    }
  }
}
