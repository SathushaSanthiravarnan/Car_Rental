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
    public class CarStatusConfiguration : IEntityTypeConfiguration<CarStatus>
    {
        public void Configure(EntityTypeBuilder<CarStatus> b)
        {
            b.ToTable("CarStatuses");

            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            b.Property(x => x.Description)
                .HasMaxLength(250);

            b.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
