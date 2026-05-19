using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification
{

    public class ProductSpecification : BaseSpecification<Product>
    {
    
        public ProductSpecification(string? brand, string? type,string? sort) : base(p =>
            (string.IsNullOrEmpty(brand) || p.Brand == brand) &&
            (string.IsNullOrEmpty(type) || p.Type == type))
        {
            
            switch (sort?.ToLower().Trim())
            {
                case "priceasc":
                    AddOrderBy(p => p.Price);
                    break;
                case "pricedesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
            

        }

    }
    
    
    
    
}








   