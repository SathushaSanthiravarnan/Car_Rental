using CarRentalSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Interfaces.Persistence
{
    /// <summary>
    /// Abstraction over the EF Core DbContext so Application layer
    /// doesn't depend on Infrastructure directly.
    /// </summary>
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Staff> Staffs { get; }
        DbSet<EmailVerification> EmailVerifications { get; }
        DbSet<StaffInvite> StaffInvites { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<CarCategory> CarCategories { get; }
        DbSet<CarModel> CarModels { get; }
        DbSet<Manufacturer> Manufacturers { get; }
        DbSet<CarStatus> CarStatuses { get; }
        DbSet<Car> Cars { get; }
        DbSet<TestDrive> TestDrives { get; }
        DbSet<Booking> Bookings { get; }
        DbSet<Payment> Payments { get; }

       DbSet<Inspection> Inspections { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}


// CarRentalSystem.Application/Interfaces/Persistence/IApplicationDbContext.cs
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using CarRentalSystem.Domain.Entities;

//namespace CarRentalSystem.Application.Interfaces.Persistence
//{
//    public interface IApplicationDbContext
//    {
//        DbSet<User> Users { get; }
//        DbSet<Customer> Customers { get; }
//        DbSet<Staff> Staffs { get; }
//        DbSet<EmailVerification> EmailVerifications { get; }
//        DbSet<StaffInvite> StaffInvites { get; }

//        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
//    }
//}
