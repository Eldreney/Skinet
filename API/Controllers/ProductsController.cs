using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
   
   
   

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
    {   
       
      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? Brand, string? Type, string? sort)
        {
           var spec = new ProductSpecification(Brand, Type, sort);
            var products = await repo.ListAsync(spec);
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }



        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);
            await repo.SaveAllAsync();
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id || !ProductExists(id))
                return BadRequest("Product not found");
            
           repo.Update(product);
            await repo.SaveAllAsync();
            
            return NoContent();
        }


    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null)
                return NotFound("Product not found");
            
            repo.Remove(product);
            await repo.SaveAllAsync();
            return NoContent();
        }


        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {   

            var spec = new BrandListSpecification();
            var brands = await repo.ListAsync(spec);
            return Ok(brands);
        }


        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            var types = await repo.ListAsync(spec);
            return Ok(types);
        }

        private bool ProductExists(int id)
        {
           return repo.Exists(id);
        }





    }
}