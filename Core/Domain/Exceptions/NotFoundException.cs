using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public abstract class NotFoundException (string message)
        :Exception(message);
    public sealed class ProductNotFoundException(int id)
    : NotFoundException($"Product With Id {id} Not Found!!");
    public sealed class UserNotFoundException(string email)
    : NotFoundException($"No User With Email {email} Was found !!");
    public sealed class AddressNotFoundException(string UserName)
     : NotFoundException($"User {UserName} has No Address ");
    public sealed class DeliveryNotFoundException(int id)
      : NotFoundException($"No Delivery Method with Id {id} was found");
}
