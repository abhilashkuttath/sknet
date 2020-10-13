using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   
    public class ProductsController:BaseApiController
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository repo,IMapper mapper)
        {
            _repo=repo;
            _mapper=mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>>  GetProducts()
        {
            var products=await _repo.GetProductsAsync();
         // return Ok(products);
         return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductToReturnDto>>GetProduct(int id)
        {
            var product= await _repo.GetProductbyIdAsync(id);
            return _mapper.Map<Product,ProductToReturnDto>(product);
        }

        [HttpGet]
        [Route("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrand()
        {
            return Ok( await _repo.GetProductBrandsAsync());
        }

         [HttpGet]
        [Route("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok( await _repo.GetProductTypesAsync());
        }

  

        
    }
}