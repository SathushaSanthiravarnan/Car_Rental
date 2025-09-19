//using CarRentalSystem.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CarRentalSystem.Infrastructure.Persistence
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options) { }

//        public DbSet<User> Users { get; set; }
//        public DbSet<Staff> Staffs { get; set; }
//        public DbSet<Customer> Customers { get; set; }
//        public DbSet<EmailVerification> EmailVerifications { get; set; }
//        public DbSet<StaffInvite> StaffInvites { get; set; }

//        //public DbSet<User> Users { get; set; } = default!;
//        //public DbSet<Staff> Staffs { get; set; } = default!;           // ← renamed
//        //public DbSet<Customer> Customers { get; set; } = default!;
//        //public DbSet<EmailVerification> EmailVerifications { get; set; } = default!;
//        //public DbSet<StaffInvite> StaffInvites { get; set; } = default!;

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            // User - Staff (1:1)
//            modelBuilder.Entity<User>()
//                .HasOne(u => u.StaffProfile)
//                .WithOne(s => s.User)
//                .HasForeignKey<Staff>(s => s.UserId);

//            // User - Customer (1:1)
//            modelBuilder.Entity<User>()
//                .HasOne(u => u.CustomerProfile)
//                .WithOne(c => c.User)
//                .HasForeignKey<Customer>(c => c.UserId);

//            // Configure EmailVerification
//            modelBuilder.Entity<EmailVerification>()
//                .HasOne(e => e.User)
//                .WithMany()
//                .HasForeignKey(e => e.UserId)
//                .OnDelete(DeleteBehavior.Cascade);

//            // Global Query Filters for Soft Delete
//            modelBuilder.Entity<User>()
//                .HasQueryFilter(e => !e.IsDeleted);

//            modelBuilder.Entity<Customer>()
//                .HasQueryFilter(e => !e.IsDeleted);

//            modelBuilder.Entity<Staff>()
//                .HasQueryFilter(e => !e.IsDeleted);
//        }
//    }
//}

// BEFORE LAST

// Infrastructure/Persistence/ApplicationDbContext.cs
//using CarRentalSystem.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using System.Reflection;

//namespace CarRentalSystem.Infrastructure.Persistence;

//public class ApplicationDbContext : DbContext
//{
//    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//        : base(options) { }

//    public DbSet<User> Users { get; set; } = default!;
//    public DbSet<Customer> Customers { get; set; } = default!;
//    public DbSet<Staff> Staffs { get; set; } = default!;
//    public DbSet<EmailVerification> EmailVerifications { get; set; } = default!;
//    public DbSet<StaffInvite> StaffInvites { get; set; } = default!;

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        base.OnModelCreating(modelBuilder);

//        //  Apply all IEntityTypeConfiguration<> in this assembly
//        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

//        //  Global query filters (soft delete)
//        modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
//        modelBuilder.Entity<Customer>().HasQueryFilter(x => !x.IsDeleted);
//        modelBuilder.Entity<Staff>().HasQueryFilter(x => !x.IsDeleted);
//    }
//}


// LAST =============

using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CarRentalSystem.Infrastructure.Persistence
{
    /// <summary>
    /// EF Core DbContext implementation of IApplicationDbContext.
    /// Handles persistence for CarRentalSystem domain entities.
    /// </summary>
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Staff> Staffs { get; set; } = default!;
        public DbSet<EmailVerification> EmailVerifications { get; set; } = default!;
        public DbSet<StaffInvite> StaffInvites { get; set; } = default!;
        public DbSet<CarCategory> CarCategories { get; set; } = default!;
        public DbSet<CarModel> CarModels { get; set; } = default!;
        public DbSet<Manufacturer> Manufacturers { get; set; } = default!;
        public DbSet<CarStatus> CarStatuses { get; set; } = default!;
        public DbSet<Car> Cars { get; set; } = default!;
        public DbSet<TestDrive> TestDrives { get; set; } = default!;
        public DbSet<Booking> Bookings { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<Inspection> Inspections { get; set; } = default!;
        //     public DbSet<Inspection> Inspections { get; set; } = default!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply IEntityTypeConfiguration<> classes automatically
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Global Query Filters for Soft Delete
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Customer>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Staff>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<CarCategory>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<CarModel>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Manufacturer>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<CarStatus>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Car>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<TestDrive>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Booking>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Payment>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Inspection>().HasQueryFilter(x => !x.IsDeleted);
            // Optional: configure Address as owned type if you want EF to embed it
            // modelBuilder.Entity<User>().OwnsOne(u => u.Address);

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Optional: automatically set audit fields
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedAtUtc = DateTime.UtcNow;

                if (entry.State == EntityState.Modified)
                    entry.Entity.UpdatedAtUtc = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}


// Last 02 ===========

// CarRentalSystem.Infrastructure/Persistence/ApplicationDbContext.cs
//using System.Reflection;
//using Microsoft.EntityFrameworkCore;
//using CarRentalSystem.Domain.Entities;
//using CarRentalSystem.Application.Interfaces.Persistence;

//namespace CarRentalSystem.Infrastructure.Persistence
//{
//    public class ApplicationDbContext : DbContext, IApplicationDbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options) { }

//        public DbSet<User> Users { get; set; } = default!;
//        public DbSet<Customer> Customers { get; set; } = default!;
//        public DbSet<Staff> Staffs { get; set; } = default!;
//        public DbSet<EmailVerification> EmailVerifications { get; set; } = default!;
//        public DbSet<StaffInvite> StaffInvites { get; set; } = default!;

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

//            // soft-delete filters (இருந்தால்)
//            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
//            modelBuilder.Entity<Customer>().HasQueryFilter(x => !x.IsDeleted);
//            modelBuilder.Entity<Staff>().HasQueryFilter(x => !x.IsDeleted);
//        }
//    }
//}
