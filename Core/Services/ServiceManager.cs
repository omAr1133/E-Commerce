using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;

namespace Services
{
    public class ServiceManager (IMapper mapper,IUnitOfWork unitOfWork, IBasketRepository basketRepository,
        UserManager<ApplicationUser> userManager,IOptions<JWTOptions> options)
        : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService =
        new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService ProductService => _lazyProductService.Value;
         

        private readonly Lazy<IBasketService> _lazyBasketService =
        new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        public IBasketService BasketService => _lazyBasketService.Value;

        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService =
        new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,options));

        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
    }
}
