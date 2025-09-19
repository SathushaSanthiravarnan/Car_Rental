using CarRentalSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.ToTable("Users");

            b.HasKey(x => x.Id);

            b.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            b.Property(x => x.LastName).HasMaxLength(100).IsRequired();

            b.Property(x => x.Email).HasMaxLength(256).IsRequired();
            b.HasIndex(x => x.Email).IsUnique();

            b.Property(x => x.Username).HasMaxLength(100);
            b.HasIndex(x => x.Username).IsUnique().HasFilter("[Username] IS NOT NULL");

            b.Property(x => x.Phone).HasMaxLength(25);

            // Password
            b.Property(x => x.PasswordHash).HasMaxLength(512).IsRequired();
            b.Property(x => x.PasswordSalt).HasMaxLength(512).IsRequired();

            // Google
            b.Property(x => x.GoogleEmail).HasMaxLength(256);
            b.Property(x => x.GoogleSubjectId).HasMaxLength(128);
            b.HasIndex(x => x.GoogleSubjectId).IsUnique().HasFilter("[GoogleSubjectId] IS NOT NULL");

            // Address as owned value object
            b.OwnsOne(x => x.Address, nav =>
            {
                nav.Property(p => p.Street).HasMaxLength(200);
                nav.Property(p => p.City).HasMaxLength(100);
                nav.Property(p => p.State).HasMaxLength(100);
                nav.Property(p => p.PostalCode).HasMaxLength(20);
                nav.Property(p => p.Country).HasMaxLength(100);
            });

            // 1:1 links (no cascade delete on user soft-delete)
            b.HasOne(x => x.StaffProfile)
             .WithOne(s => s.User)
             .HasForeignKey<Staff>(s => s.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.CustomerProfile)
             .WithOne(c => c.User)
             .HasForeignKey<Customer>(c => c.UserId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
