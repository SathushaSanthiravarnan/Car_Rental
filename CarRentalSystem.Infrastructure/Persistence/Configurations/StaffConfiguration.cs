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
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> b)
        {
            b.ToTable("Staff");
            b.HasKey(x => x.Id);

            b.Property(x => x.EmailForWork).HasMaxLength(256);
            b.Property(x => x.EmployeeCode).HasMaxLength(50);
            b.Property(x => x.Notes).HasMaxLength(1000);

            b.HasIndex(x => x.UserId).IsUnique(); // 1:1
        }
    }
}
