using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
    {
        public ProductWithBrandAndTypeSpecifications(int id)
            : base(product=>product.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        public ProductWithBrandAndTypeSpecifications(int?brandId,int?typeId, ProductSortingOptions options)
            :base(product=>
            (!brandId.HasValue || product.BrandId==brandId.Value)&&
            (!typeId.HasValue || product.TypeId==typeId.Value))
        {
            AddInclude(p=>p.ProductBrand);
            AddInclude(p=>p.ProductType);

            switch(options)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p=>p.Name);
                    break;

                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;

                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }

        }
    }
}
