using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Products;
using Microsoft.Extensions.Configuration;

namespace Services.MappingProfiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(d => d.BrandName,
                options => options.MapFrom(s => s.ProductBrand.Name))
                 .ForMember(d => d.TypeName,
                options => options.MapFrom(s => s.ProductType.Name))
                 .ForMember(d => d.PictureUrl
                 , options => options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandResponse>();
            CreateMap<ProductType, TypeResponse>();


        }

    }
    internal class PictureUrlResolver(IConfiguration configuration)
        : IValueResolver<Product, ProductResponse, string>
    {
        public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.PictureUrl))
            {
                return $"{configuration["BaseUrl"]}{source.PictureUrl}";
            }
            return "";
                
        }
    }

}
