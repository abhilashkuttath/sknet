using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;



namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context=context;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(int page,int pageSize,string sort,int? brandId=null,int? typeId=null,string search=null)
        {
           var productsQuery= _context.Products
          .Include( p => p.ProductBrand)
         .Include( p => p.ProductType).AsQueryable();
         if(!string.IsNullOrEmpty(search))
         productsQuery=productsQuery.Where(v => v.Name.ToLower().Contains(search.ToLower()));
         if(brandId.HasValue)
         productsQuery=productsQuery.Where(v => v.ProductBrandId == brandId );
         if(typeId.HasValue)
         productsQuery=productsQuery.Where(v => v.ProductTypeId == typeId );

          if(!string.IsNullOrEmpty(sort))
           {
               switch(sort)
                {
                    case "priceAsc":
                    productsQuery =productsQuery.OrderBy(o => o.Price);
                    break;
                    case "priceDsc":
                    productsQuery =productsQuery.OrderByDescending(o => o.Price);
                   break;
                    default:
                    productsQuery =productsQuery.OrderBy(o => o.Name);
                    break;

                }
            }
            if(pageSize <=0)
            pageSize=10;
            productsQuery=productsQuery.Skip((page-1) * pageSize).Take(pageSize);

         return await productsQuery.ToListAsync();


        // //    return await _context.Products
        // //    .Include( p => p.ProductBrand)
        // //    .Include( p => p.ProductType).OrderBy(o => o.Name)
           
        // //    .ToListAsync();
         
        //  IQueryable<Product> productsQuery=  _context.Products
        //   .Include( p => p.ProductBrand)
        //    .Include( p => p.ProductType);
        // // .Where(w => w.ProductBrandId == brandId|| !brandId.HasValue    && w.ProductTypeId == typeId ||!typeId.HasValue)
          
        //    if(!string.IsNullOrEmpty(sort))
        //    {
        //        switch(sort)
        //        {
        //            case "priceAsc":
        //            productsQuery =productsQuery.OrderBy(o => o.Price);
        //            break;
        //            case "priceDsc":
        //            productsQuery =productsQuery.OrderByDescending(o => o.Price);
        //            break;
        //            default:
        //            productsQuery =productsQuery.OrderBy(o => o.Name);
        //            break;

        //        }
        //    }
        //     if(brandId.HasValue)
        //  productsQuery= productsQuery.Where(b =>b.ProductBrandId==brandId);
        //    if(typeId.HasValue)
        //   productsQuery= productsQuery.Where(b =>b.ProductTypeId==typeId);
        //    return await productsQuery.ToListAsync();
        }

         public async Task<Product> GetProductbyIdAsync(int id)
        {
            return await _context.Products
            .Include( p => p.ProductBrand)
            .Include( p => p.ProductType)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

       

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}