using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Products;

namespace ServicesAbstractions
{
    public interface IProductService
    {
        //Get All Products => IEnumerable <ProductResponse>
        Task<PaginatedResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters queryParameters);
        //Get Product
        Task <ProductResponse> GetProductAsync(int id);
        //Get All Brands
        Task <IEnumerable<BrandResponse>> GetBrandsAsync();
        //Get All Types
        Task<IEnumerable<TypeResponse>> GetTypesAsync();

        
    }
}
