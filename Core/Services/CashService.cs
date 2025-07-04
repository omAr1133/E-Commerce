using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    internal class CashService(ICashRepository cashRepository)
        : ICashService
    {
        public async Task<string?> GetAsync(string cashKey)
        =>await cashRepository.GetAsync(cashKey);

        public async Task SetAsync(string Key, object Value, TimeSpan expiration)
        {
            await cashRepository.SetAsync(Key,JsonSerializer.Serialize(Value), expiration);
        }
    }
}
