﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class ServiceManagerWithFactoryDelegate(Func<IProductService> productFactory,
        Func<IAuthenticationService> authFactory,
        Func<IOrderService> orderFactory,
        Func<IBasketService> basketFactory)
        : IServiceManager
    {
        public IProductService ProductService => productFactory.Invoke();
        public IBasketService BasketService => basketFactory.Invoke();

        public IAuthenticationService AuthenticationService => authFactory.Invoke();

        public IOrderService OrderService => orderFactory.Invoke();
    }
}
