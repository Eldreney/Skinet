using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(string? Brand,string? Type,string? Sort);
        Task<Product?> GetProductByIdAsync(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        bool ProductExists(int id);
        Task<bool> SaveChangesAsync();
        Task<IReadOnlyList<string>> GetbrandAsync(); 
        Task<IReadOnlyList<string>> GetTypesAsync();
    }
}