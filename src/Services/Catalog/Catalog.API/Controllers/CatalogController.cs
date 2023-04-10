using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Catalog.API.Controllers
{
  [ApiController]
  [Route("api/v1/[controller]")]
  public class CatalogController : ControllerBase
  {
    private readonly IProductRepository _repository;
    private readonly ILogger<CatalogController> _logger;
    private readonly TextInfo _textInfo = CultureInfo.CurrentCulture.TextInfo;

    public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
    {
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var products = await _repository.GetProducts();
      return Ok(products);
    }

    [HttpGet("{id:length(36)}", Name = "GetProduct")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var product = await _repository.GetProduct(id);
      if (product == null)
      {
        _logger.LogError($"Product with id: {id}, not found");
        return NotFound();
      }
      return Ok(product);
    }

    [Route("[action]/{category}", Name = "GetProductByCategory")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var products = await _repository.GetProductByCategory(category);
      return Ok(products);
    }

    [Route("[action]", Name = "GetProductByName")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductByName([FromQuery] string name)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var products = await _repository.GetProductByName(name);
      return Ok(products);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductDto productRequest)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      if (productRequest != null)
      {
        var product = new Product
        {
          Id = Guid.NewGuid().ToString(),
          Name = _textInfo.ToLower(productRequest.Name!),
          Category = _textInfo.ToLower(productRequest.Category!),
          Summary = productRequest.Summary,
          Description = productRequest.Description,
          ImageFile = productRequest.ImageFile,
          Price = productRequest.Price
        };
        await _repository.CreateProduct(productRequest);
      }
      return CreatedAtRoute("GetProductByName", new { name = productRequest!.Name });
    }

    [HttpPut]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(ProductDto productRequest)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      if (productRequest != null)
      {
        var product = new Product
        {
          Id = Guid.NewGuid().ToString(),
          Name = _textInfo.ToLower(productRequest.Name!),
          Category = _textInfo.ToLower(productRequest.Category!),
          Summary = productRequest.Summary,
          Description = productRequest.Description,
          ImageFile = productRequest.ImageFile,
          Price = productRequest.Price,
          UpdateAt = DateTime.UtcNow,
        };
        return Ok(await _repository.UpdateProduct(productRequest));
      }
      return NotFound();
    }

    [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProductById(string id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      return Ok(await _repository.DeleteProduct(id));
    }
  }
}
