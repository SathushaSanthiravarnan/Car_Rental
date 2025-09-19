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
    public class InspectionConfiguration : IEntityTypeConfiguration<Inspection>
    {
        public void Configure(EntityTypeBuilder<Inspection> builder)
        {
            builder.ToTable("Inspections");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.InspectionType)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(i => i.Notes)
                .HasMaxLength(1000);

            builder.Property(i => i.Date)
                .IsRequired();

            builder.HasOne(i => i.Car)
                .WithMany()
                .HasForeignKey(i => i.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Booking)
                .WithMany()
                .HasForeignKey(i => i.BookingId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(i => i.Staff)
                .WithMany()
                .HasForeignKey(i => i.StaffId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
