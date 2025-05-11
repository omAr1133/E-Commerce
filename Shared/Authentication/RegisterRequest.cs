using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Authentication
{
    public record RegisterRequest(string Email, string Password, string DisplayName, string UserName,string PhoneNumber) 
    {
    }
}
