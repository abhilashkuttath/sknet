using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductbyIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync(int page,int pageSize,string sort,int? brandId,int? typeId,string search); 
        
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync(); 
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync(); 

    }
}