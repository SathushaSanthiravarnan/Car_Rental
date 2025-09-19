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
    public class TestDriveConfiguration : IEntityTypeConfiguration<TestDrive>
    {
        public void Configure(EntityTypeBuilder<TestDrive> builder)
        {
            // Table name
            builder.ToTable("TestDrives");

            // Primary Key
            builder.HasKey(td => td.Id);

            // Property configurations
            builder.Property(td => td.PreferredDate)
                .IsRequired();

            builder.Property(td => td.PreferredTime)
                .IsRequired();

            builder.Property(td => td.DurationMinutes)
                .IsRequired();

            builder.Property(td => td.Status)
                .IsRequired();

            builder.Property(td => td.Remarks)
                .HasMaxLength(500);

            // Foreign key relationships
            builder.HasOne(td => td.Customer)
                .WithMany()
                .HasForeignKey(td => td.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(td => td.Car)
                .WithMany()
                .HasForeignKey(td => td.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(td => td.ApprovedByStaff)
                .WithMany()
                .HasForeignKey(td => td.ApprovedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Soft Delete filter
            builder.HasQueryFilter(td => !td.IsDeleted);
        }
    }
}
