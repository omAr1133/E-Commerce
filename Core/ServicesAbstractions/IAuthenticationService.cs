using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Authentication;

namespace ServicesAbstractions
{
    public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(LoginRequest request);

        Task<UserResponse> RegisterAsync(RegisterRequest request);

        Task<bool> CheckEmailAsync (string email);

        Task<AddressDTO> GetUserAddressAsync(string address);
        Task<AddressDTO> UpdateUserAddressAsync(AddressDTO addressDTO,string address);
        Task<UserResponse>GetUserByEmail(string email);


    }
}
