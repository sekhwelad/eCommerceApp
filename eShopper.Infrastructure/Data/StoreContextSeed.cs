using eShopper.Core.Entities;
using eShopper.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eShopper.Infrastructure.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if(!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("C:\\Users\\user-pc\\Documents\\Udemy\\2023\\e-Commerce App with .Net Core & Angular\\Project\\WebAPI\\eCommerceApp\\eShopper.Infrastructure\\Data\\SeedData\\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands);
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("C:\\Users\\user-pc\\Documents\\Udemy\\2023\\e-Commerce App with .Net Core & Angular\\Project\\WebAPI\\eCommerceApp\\eShopper.Infrastructure\\Data\\SeedData\\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
            }

            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("C:\\Users\\user-pc\\Documents\\Udemy\\2023\\e-Commerce App with .Net Core & Angular\\Project\\WebAPI\\eCommerceApp\\eShopper.Infrastructure\\Data\\SeedData\\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }

            if (!context.DeliveryMethods.Any())
            {
                var deliveryData = File.ReadAllText("C:\\Users\\user-pc\\Documents\\Udemy\\2023\\e-Commerce App with .Net Core & Angular\\Project\\WebAPI\\eCommerceApp\\eShopper.Infrastructure\\Data\\SeedData\\delivery.json");
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                context.DeliveryMethods.AddRange(methods);
            }


            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}
