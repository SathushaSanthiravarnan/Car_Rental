using System.Reflection;
using CarRentalSystem.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalSystem.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Registers Application-layer services:
    ///  - MediatR (CQRS)
    ///  - AutoMapper (object mapping)
    ///  - FluentValidation (validators)
    ///  - Pipeline behaviors (Validation, Logging, etc.)
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // --------------------------
        // MediatR (v12+)
        // --------------------------
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);

            // if you want, you can auto-register pipeline behaviors here
            // cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // --------------------------
        // AutoMapper
        // --------------------------
        services.AddAutoMapper(assembly);

        // --------------------------
        // FluentValidation
        // --------------------------
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        // --------------------------
        // Pipeline Behaviors
        // Order matters if you chain more (Validation → Logging → Transaction, etc.)
        // --------------------------
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}