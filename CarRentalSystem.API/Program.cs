using CarRentalSystem.Application;
using CarRentalSystem.Infrastructure;
using CarRentalSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register Application & Infrastructure services
            builder.Services.AddApplication();
            // Add Infrastructure (DbContext, etc.)
            builder.Services.AddInfrastructure(builder.Configuration,
                builder.Environment.IsDevelopment());

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
