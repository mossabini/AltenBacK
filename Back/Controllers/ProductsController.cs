using Back.Data;
using Back.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = ProductData.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = ProductData.GetProductById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            ProductData.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            var existingProduct = ProductData.GetProductById(id);
            if (existingProduct == null)
                return NotFound();

            product.Id = existingProduct.Id;
            ProductData.UpdateProduct(product);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceProduct(int id, Product product)
        {
            var existingProduct = ProductData.GetProductById(id);
            if (existingProduct == null)
                return NotFound();

            product.Id = existingProduct.Id;
            ProductData.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var existingProduct = ProductData.GetProductById(id);
            if (existingProduct == null)
                return NotFound();

            ProductData.DeleteProduct(id);
            return NoContent();
        }
    }
}
