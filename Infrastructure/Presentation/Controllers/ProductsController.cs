using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DataTransferObjects.Products;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController (IServiceManager serviceManager)
        :ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts(int? brandId ,int? typeId) //Get BaseUrl/api/Products
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync(brandId,typeId);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(int id) //Get BaseUrl/api/Products/{id}
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);
            return Ok(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<BrandResponse>> GetBrands() //Get BaseUrl/api/Products/brands
        {
            var brands = await serviceManager.ProductService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<BrandResponse>> GetTypes() //Get BaseUrl/api/Products/types
        {
            var types = await serviceManager.ProductService.GetTypesAsync();
            return Ok(types);
        }


    }
}
