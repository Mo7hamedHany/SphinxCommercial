using SphinxCommercial.Core.Models;
using SphinxCommercial.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SphinxCommercial.Repository.Data.DataSeeding
{
    public static class DataContextSeed
    {

        public static async Task SeedData(AppDataContext context)
        {
            if (!context.Set<Client>().Any())
            {

                var clientsData = await File.ReadAllTextAsync("../SphinxCommercial.Repository/Data/DataSeeding/Clients.json");

                var clients = JsonSerializer.Deserialize<List<Client>>(clientsData);

                if (clients != null && clients.Any())
                {
                    await context.Set<Client>().AddRangeAsync(clients);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Set<Product>().Any())
            {

                var productsData = await File.ReadAllTextAsync("../SphinxCommercial.Repository/Data/DataSeeding/Products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products != null && products.Any())
                {
                    await context.Set<Product>().AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Set<ClientProduct>().Any())
            {

                var clientProductsData = await File.ReadAllTextAsync("../SphinxCommercial.Repository/Data/DataSeeding/ClientProducts.json");

                var clientProducts = JsonSerializer.Deserialize<List<ClientProduct>>(clientProductsData);

                if (clientProducts != null && clientProducts.Any())
                {
                    await context.Set<ClientProduct>().AddRangeAsync(clientProducts);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
