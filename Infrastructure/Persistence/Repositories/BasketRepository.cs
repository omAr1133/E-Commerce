using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Basket;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection)
        : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task DeleteAsync(string id) => await _database.KeyDeleteAsync(id);

        public async Task<CustomerBasket?> GetAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);

            if (basket.IsNullOrEmpty) return null;
            return JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }

        public async Task<CustomerBasket?> UpdateAsync(CustomerBasket Basket, TimeSpan? timeSpan = null)
        {
            var JsonBasket = JsonSerializer.Serialize(Basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(Basket.Id, JsonBasket,timeSpan ?? TimeSpan.FromDays(7));
            return IsCreatedOrUpdated ? await GetAsync(Basket.Id) : null;

        }
    }
}
