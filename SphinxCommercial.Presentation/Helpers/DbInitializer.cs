using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SphinxCommercial.Repository.Data.Contexts;
using SphinxCommercial.Repository.Data.DataSeeding;

namespace SphinxCommercial.Presentation.Helpers
{
    public static class DbInitializer
    {
        public static async Task InitializeDbAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;

                var loggerFactory = service.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = service.GetRequiredService<AppDataContext>();

                    if ((await context.Database.GetPendingMigrationsAsync()).Any())
                    {
                        await context.Database.MigrateAsync();
                    }

                    await DataContextSeed.SeedData(context);

                }
                catch (Exception ex)
                {

                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
