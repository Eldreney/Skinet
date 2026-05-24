using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification
{

    public class ProductSpecification : BaseSpecification<Product>
    {
    
        public ProductSpecification(ProductSpecParams specParams) : base(p =>
            (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)) &&
            (specParams.Brands.Count == 0 || specParams.Brands.Contains(p.Brand)) &&
            (specParams.Types.Count == 0 || specParams.Types.Contains(p.Type)))
        {

            ApplyPaging((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
            
            switch (specParams.Sort?.ToLower().Trim())
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








   