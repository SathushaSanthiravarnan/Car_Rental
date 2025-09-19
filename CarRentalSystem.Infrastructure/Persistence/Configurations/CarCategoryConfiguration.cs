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
    public class CarCategoryConfiguration : IEntityTypeConfiguration<CarCategory>
    {
        public void Configure(EntityTypeBuilder<CarCategory> b)
        {
            // Sets the table name
            b.ToTable("CarCategories");

            // Configures the primary key
            b.HasKey(x => x.Id);

            // Configures the 'Name' property as required with a max length
            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configures the 'Description' property with a max length
            b.Property(x => x.Description)
                .HasMaxLength(500);

            // Adds a unique index on the 'Name' to prevent duplicate category names
            b.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
