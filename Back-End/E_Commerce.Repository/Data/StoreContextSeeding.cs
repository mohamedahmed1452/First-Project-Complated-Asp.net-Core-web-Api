using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeeding
    {

        public static async Task SeedingAsync(StoreContext dbContext)
        {


            if (!dbContext.ProductBrands.Any())//true if one element inside collection
            {
                var brandsData = File.ReadAllText("../E_Commerce.Repository/Data/DataSeeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                        await dbContext.ProductBrands.AddAsync(brand);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.ProductCategories.Any())//true if one element inside collection
            {
                var TypesData = File.ReadAllText("../E_Commerce.Repository/Data/DataSeeding/Categories.json");
                var tpyes = JsonSerializer.Deserialize<List<ProductCategory>>(TypesData);
                if (tpyes?.Count > 0)
                {
                    foreach (var type in tpyes)
                        await dbContext.ProductCategories.AddAsync(type);
                    await dbContext.SaveChangesAsync();

                }
            }
            if (!dbContext.Products.Any())//true if one element inside collection
            {
                var Products = File.ReadAllText("../E_Commerce.Repository/Data/DataSeeding/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(Products);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                        await dbContext.Products.AddAsync(product);
                    await dbContext.SaveChangesAsync();

                }
            }
            if (dbContext.DeliveryMethods.Count()==0)//true if one element inside collection
            {
                var deliveryMethodsData = File.ReadAllText("../E_Commerce.Repository/Data/DataSeeding/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);
                if (deliveryMethods?.Count > 0)
                {
                    foreach (var delivery in deliveryMethods)
                        await dbContext.DeliveryMethods.AddAsync(delivery);
                    await dbContext.SaveChangesAsync();

                }
            }


        }

    }
}
