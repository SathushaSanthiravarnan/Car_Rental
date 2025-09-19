/*
namespace CarRentalSystem.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Swagger service add
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
*/
/*
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace CarRentalSystem.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Services
            builder.Services.AddControllers();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}

*/
//using CarRentalSystem.WebApi; // Extension method (AddWebApi) import

//namespace CarRentalSystem.WebApi
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Register all layers + WebApi configs via DI extension
//            builder.Services.AddWebApi(builder.Configuration);

//            var app = builder.Build();

//            // Swagger (only in Development)
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();

//            // CORS
//            app.UseCors("AllowAll");

//            // Authentication & Authorization
//            app.UseAuthentication();
//            app.UseAuthorization();

//            // Map controllers
//            app.MapControllers();

//            app.Run();
//        }
//    }
//}


using CarRentalSystem.Application;
using CarRentalSystem.Infrastructure;
using CarRentalSystem.WebApi;
using CarRentalSystem.WebApi.ExceptionHandlers;
using CarRentalSystem.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Register Application Layer
builder.Services.AddApplication();

// Register Infrastructure Layer
builder.Services.AddInfrastructure(builder.Configuration);

// Register WebApi Layer
builder.Services.AddWebApiServices(builder.Configuration);

// NEW VALIDATION FROM EXCEPTION HANDLER ===============================

// Services registration
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// =====================================================================

var app = builder.Build();


// FOR VALIDATION ======================================================

// global error handling
//app.UseMiddleware<ExceptionMiddleware>();

// ---------------------------------------------------------------------

// NEW VALIDATION FROM EXCEPTION HANDLER ===============================

app.UseExceptionHandler();

// =====================================================================

// Swagger (only in Development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRentalSystem API v1");
        options.RoutePrefix = string.Empty; // Swagger opens at root URL
    });
}

// HTTPS redirection
app.UseHttpsRedirection();

// CORS
app.UseCors("DefaultCors");

// Authentication + Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Health check endpoint
app.MapHealthChecks("/health");

// Run the application
app.Run();


