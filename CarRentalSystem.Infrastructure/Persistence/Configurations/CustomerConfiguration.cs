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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> b)
        {
            b.ToTable("Customers");
            b.HasKey(x => x.Id);

            b.Property(x => x.EmailForContact).HasMaxLength(256);
            b.Property(x => x.AlternatePhone).HasMaxLength(25);
            b.Property(x => x.NationalIdNumber).HasMaxLength(100);
            b.Property(x => x.DrivingLicenseNumber).HasMaxLength(100);

            b.HasIndex(x => x.UserId).IsUnique(); // 1:1 mapping

            // Relation back to User is configured in UserConfiguration as FK on Customer.UserId
        }
    }
}
