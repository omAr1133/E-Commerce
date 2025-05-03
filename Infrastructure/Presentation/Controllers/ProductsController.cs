

using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{

    public class ProductsController (IServiceManager serviceManager)
        :APIController
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<ProductResponse>>> GetProducts([FromQuery]ProductQueryParameters queryParameters) //Get BaseUrl/api/Products
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync(queryParameters);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> Get(int id) //Get BaseUrl/api/Products/{id}
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);
            return Ok(product);
        }

       // [Authorize(Roles="Admin")]
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
