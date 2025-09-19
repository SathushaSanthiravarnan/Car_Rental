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
    public class CarModelConfiguration : IEntityTypeConfiguration<CarModel>
    {
        public void Configure(EntityTypeBuilder<CarModel> b)
        {
            // Table name and primary key configuration
            b.ToTable("CarModels");
            b.HasKey(x => x.Id);

            // Property configurations with data constraints
            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            b.Property(x => x.BodyType)
                .IsRequired()
                .HasMaxLength(50);

            b.Property(x => x.FuelType)
                .IsRequired()
                .HasMaxLength(50);

            b.Property(x => x.Transmission)
                .IsRequired()
                .HasMaxLength(50);

            // Foreign key relationships
            b.HasOne(x => x.Manufacturer)
                .WithMany()
                .HasForeignKey(x => x.ManufacturerId)
                .OnDelete(DeleteBehavior.Cascade); // Deleting a manufacturer deletes all their car models.

            b.HasOne(x => x.CarCategory)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents a category from being deleted if car models are associated with it.
        }
    }
}
