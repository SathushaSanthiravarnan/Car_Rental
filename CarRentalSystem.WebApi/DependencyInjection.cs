//using CarRentalSystem.Application;
//using CarRentalSystem.Infrastructure;

//namespace CarRentalSystem.WebApi
//{
//    public static class DependencyInjection
//    {
//        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
//        {
//            // Application Layer registrations
//            services.AddApplication();

//            // Infrastructure Layer registrations
//            services.AddInfrastructure(configuration);

//            // Controllers
//            services.AddControllers();

//            // Swagger
//            services.AddEndpointsApiExplorer();
//            services.AddSwaggerGen();

//            // CORS
//            services.AddCors(options =>
//            {
//                options.AddPolicy("AllowAll", builder =>
//                    builder.AllowAnyOrigin()
//                           .AllowAnyMethod()
//                           .AllowAnyHeader());
//            });

//            return services;
//        }
//    }
//}


// 12.09.205 ------------------------------------------------------

/*using System.Text;
using Asp.Versioning;                 // API Versioning
using Asp.Versioning.ApiExplorer;     // Versioned API Explorer (for Swagger grouping)
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalSystem.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration config)
    {
        // Controllers + JSON options
        services.AddControllers()
            .AddJsonOptions(o =>
            {
                // o.JsonSerializerOptions.PropertyNamingPolicy = null; // optional
            });

        // API Versioning (Asp.Versioning) + Explorer (note the chaining)
        services
            .AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;

                // Use URL segment versioning => /api/v{version}/...
                o.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
                // Alternatives:
                // o.ApiVersionReader = new QueryStringApiVersionReader("api-version");
                // o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            })
            .AddApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";          // v1, v2, v3
                o.SubstituteApiVersionInUrl = true;    // replaces {version:apiVersion} in routes
            });

        // Swagger / OpenAPI
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // CORS (adjust origins to your frontend)
        services.AddCors(options =>
        {
            options.AddPolicy("DefaultCors", builder =>
            {
                builder
                    .WithOrigins("http://localhost:3000", "http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        // Authentication (JWT) – optional (enabled if key present)
        var jwtKey = config["Jwt:Key"];
        var jwtIssuer = config["Jwt:Issuer"];
        var jwtAudience = config["Jwt:Audience"];

        if (!string.IsNullOrWhiteSpace(jwtKey))
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                        ValidateIssuer = !string.IsNullOrWhiteSpace(jwtIssuer),
                        ValidIssuer = jwtIssuer,
                        ValidateAudience = !string.IsNullOrWhiteSpace(jwtAudience),
                        ValidAudience = jwtAudience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });
        }

        services.AddAuthorization();
        services.AddHealthChecks();

        return services;
    }
}*/


// LAST -----------------------------------------------

using System.Text;
using Asp.Versioning;                 // API Versioning
using Asp.Versioning.ApiExplorer;     // Versioned API Explorer (for Swagger grouping)
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalSystem.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration config)
    {
        // Controllers + JSON options
        services.AddControllers()
            .AddJsonOptions(o =>
            {
                // o.JsonSerializerOptions.PropertyNamingPolicy = null; // optional
            });

        // API Versioning (Asp.Versioning) + Explorer (note the chaining)
        services
            .AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;

                // Use URL segment versioning => /api/v{version}/...
                o.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
                // Alternatives:
                // o.ApiVersionReader = new QueryStringApiVersionReader("api-version");
                // o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            })
            .AddApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";          // v1, v2, v3
                o.SubstituteApiVersionInUrl = true;    // replaces {version:apiVersion} in routes
            });

        // Swagger / OpenAPI
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // CORS (adjust origins to your frontend)
        services.AddCors(options =>
        {
            options.AddPolicy("DefaultCors", builder =>
            {
                builder
                    .WithOrigins("http://localhost:3000", "http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        // Authentication (JWT) – optional (enabled if key present)
        var jwtKey = config["Jwt:Key"];
        var jwtIssuer = config["Jwt:Issuer"];
        var jwtAudience = config["Jwt:Audience"];

        if (!string.IsNullOrWhiteSpace(jwtKey))
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                        ValidateIssuer = !string.IsNullOrWhiteSpace(jwtIssuer),
                        ValidIssuer = jwtIssuer,
                        ValidateAudience = !string.IsNullOrWhiteSpace(jwtAudience),
                        ValidAudience = jwtAudience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });
        }

        services.AddAuthorization();
        services.AddHealthChecks();

        return services;
    }
}
