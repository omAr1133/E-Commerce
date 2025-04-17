using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;

namespace Services
{
    public class ServiceManager (IMapper mapper,IUnitOfWork unitOfWork)
        : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService =
        new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));

        public IProductService ProductService => _lazyProductService.Value;
    }
}
