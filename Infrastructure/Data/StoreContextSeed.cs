using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
     
        public static async Task SeedAsync(StoreContext context)
        {
          if (!context.Products.Any())
        {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/SeedData/products.json");
        
      
        var productsData = await File.ReadAllTextAsync(path);
        
        var products = JsonSerializer.Deserialize<List<Product>>(productsData);
        
        if (products != null) 
        {
            context.Products.AddRange(products);    
            await context.SaveChangesAsync();
        }
    
    
        }
    }



    }
}