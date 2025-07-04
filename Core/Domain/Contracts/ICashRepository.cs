using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICashRepository
    {
        Task<string?> GetAsync(string cashKey);
        Task SetAsync (string cashKey, string value, TimeSpan expiration);
    }
}
