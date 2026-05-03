using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data
{

    public class ProductRepository(StoreContext context) : IProductRepository
    {

       
      
        public void AddProduct(Product product)
        {
           context.Products.Add(product);   
        }

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);
        }

        public async Task<IReadOnlyList<string>> GetbrandAsync()
        {
           return await context.Products.Select(p => p.Brand).Distinct().ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(string? Brand,string? Type,string? sort)
        {
            var query = context.Products.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(Brand))
            {
                query = query.Where(p => p.Brand == Brand);
            }

            if (!string.IsNullOrEmpty(Type))
            {
                query = query.Where(p => p.Type == Type);
            }

   
        query = sort?.ToLower().Trim() switch
        { 
            "priceasc"  => query.OrderBy(p => p.Price),
            "pricedesc" => query.OrderByDescending(p => p.Price),
            _           => query.OrderBy(p => p.Name) 
        };
    

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetTypesAsync()
        {
            return await context.Products.Select(p => p.Type).Distinct().ToListAsync();
        }

        public bool ProductExists(int id)
        {
            return context.Products.Any(p => p.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
                => await context.SaveChangesAsync() > 0;
        

        public void UpdateProduct(Product product)
        {
           context.Entry(product).State = EntityState.Modified;
        }
    }

}