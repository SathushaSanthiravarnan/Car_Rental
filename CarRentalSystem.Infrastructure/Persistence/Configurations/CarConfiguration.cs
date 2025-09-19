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
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> b)
        {
            b.ToTable("Cars");

            b.HasKey(x => x.Id);

            b.Property(x => x.PlateNumber)
                .IsRequired()
                .HasMaxLength(20);

            b.HasIndex(x => x.PlateNumber)
                .IsUnique();

            b.Property(x => x.Color)
                .HasMaxLength(50);

            b.HasOne(x => x.CarModel)
                .WithMany()
                .HasForeignKey(x => x.CarModelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.CarStatus)
                .WithMany(s => s.Cars)
                .HasForeignKey(x => x.CarStatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
