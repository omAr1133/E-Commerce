using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Authentication;

namespace Shared.Orders
{
    public record OrderRequest(string BakestId,AddressDTO Address, int DeliveryMethodId);

    }