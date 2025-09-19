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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Table name
            builder.ToTable("Payments");

            // Primary key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.Currency)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(p => p.PaymentMethod)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.TransactionId)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.PaymentDate)
                .IsRequired();

            // Enum status as a string in the database
            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Remarks)
                .HasMaxLength(500);

            // Foreign key relationship with Booking
            builder.HasOne(p => p.Booking)
                .WithMany()
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            // Optional: Ensure TransactionId is unique to prevent duplicate payments
            builder.HasIndex(p => p.TransactionId)
                .IsUnique();
        }
    }
}
