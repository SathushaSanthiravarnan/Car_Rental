using CarRentalSystem.Application.Interfaces.Notifications;          // + email iface
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Application.Interfaces.Security;
using CarRentalSystem.Application.Interfaces.Services;
using CarRentalSystem.Infrastructure.Notifications.Email;           // + email impl
using CarRentalSystem.Infrastructure.Persistence;
using CarRentalSystem.Infrastructure.Repositories;
using CarRentalSystem.Infrastructure.Security;
using CarRentalSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Map interface -> concrete DbContext for Application layer
            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IStaffInviteRepository, StaffInviteRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IEmailVerificationRepository, EmailVerificationRepository>();

            // Security / Auth
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IGoogleTokenValidator, GoogleTokenValidator>();

            // Security
            //services.AddScoped<ICustomerRepository, CustomerRepository>();
            // services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<ICarCategoryRepository, CarCategoryRepository>();
            services.AddScoped<ICarModelRepository, CarModelRepository>();
            services.AddScoped<ICarStatusRepository, CarStatusRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ITestDriveRepository, TestDriveRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IInspectionRepository, InspectionRepository>();
            // ✅ Register password hasher
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            // Email sender
            services.AddScoped<IEmailSender, SmtpEmailSender>();

            return services;
        }
    }
}