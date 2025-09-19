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
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> b)
        {
            b.ToTable("Bookings");
            b.HasKey(x => x.Id);

            // ... other properties

            // Relationships to other entities
            b.HasOne(x => x.Car).WithMany(c => c.Bookings).HasForeignKey(x => x.CarId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Customer).WithMany(c => c.Bookings).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);

            // New: Relationship to Address entity
           
        }
    }
}
