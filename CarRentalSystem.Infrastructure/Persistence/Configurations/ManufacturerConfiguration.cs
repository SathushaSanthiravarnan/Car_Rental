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
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> b)
        {
            // Sets the table name to "Manufacturers"
            b.ToTable("Manufacturers");

            // Configures the primary key
            b.HasKey(x => x.Id);

            // Configures the 'Name' property as required and sets its maximum length
            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configures the 'Country' property with a maximum length
            b.Property(x => x.Country)
                .HasMaxLength(100);

            // Adds a unique index on the 'Name' property to ensure no duplicate manufacturer names
            b.HasIndex(x => x.Name).IsUnique();

            // The 'AuditableEntity' properties (CreatedBy, CreatedAt, etc.) are handled by conventions or a base configuration.
        }
    }
}
