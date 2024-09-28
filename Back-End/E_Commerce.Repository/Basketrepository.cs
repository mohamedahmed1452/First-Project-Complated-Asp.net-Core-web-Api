using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Talabat.Repository
{
    public class Basketrepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public Basketrepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);

            // Fixed the incorrect use of IsNullOrEmpty in deserialization
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var createdForUpdate = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

            // Fetch the updated basket if successful, otherwise return null
            return createdForUpdate ? await GetBasketAsync(basket.Id) : null;
        }
    }
}
