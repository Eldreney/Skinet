using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
   
   
   

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository repo) : ControllerBase
    {   
       
      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? Brand, string? Type, string? sort)
        {
            var products = await repo.GetProductsAsync(Brand, Type, sort);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await repo.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }



        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.AddProduct(product);
            await repo.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id || !ProductExists(id))
                return BadRequest("Product not found");
            

           repo.UpdateProduct(product);
            await repo.SaveChangesAsync();
            

            return NoContent();
        }


    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Product not found");
            
            repo.DeleteProduct(product);
            await repo.SaveChangesAsync();
             return NoContent();
        }


        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var brands = await repo.GetbrandAsync();
            return Ok(brands);
        }


        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<string>>> GetTypes()
        {
            var types = await repo.GetTypesAsync();
            return Ok(types);
        }

        private bool ProductExists(int id)
        {
           return repo.ProductExists(id);
        }





    }
}