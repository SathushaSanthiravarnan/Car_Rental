using CarRentalSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Infrastructure.Configurations
{
    public class StaffInviteConfiguration : IEntityTypeConfiguration<StaffInvite>
    {
        public void Configure(EntityTypeBuilder<StaffInvite> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Email).IsRequired().HasMaxLength(150);
            b.Property(x => x.Token).IsRequired().HasMaxLength(256);
            b.HasIndex(x => x.Token).IsUnique();
            b.Property(x => x.ExpiresAt).IsRequired();
            b.Property(x => x.IsUsed).HasDefaultValue(false);
        }
    }
}
