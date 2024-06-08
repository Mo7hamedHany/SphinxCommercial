using Microsoft.EntityFrameworkCore;
using SphinxCommercial.Core.Interfaces.Repositories;
using SphinxCommercial.Core.Interfaces.Services;
using SphinxCommercial.Presentation.Helpers;
using SphinxCommercial.Repository.Data.Contexts;
using SphinxCommercial.Repository.Repositories;
using SphinxCommercial.Service;
using System.Reflection;

namespace SphinxCommercial.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<AppDataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IClientProductService, ClientProductService>();

            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IClientService, ClientService>();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            var app = builder.Build();

            await DbInitializer.InitializeDbAsync(app);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}