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
    public class StaffInviteConfiguration : IEntityTypeConfiguration<StaffInvite>
    {
        public void Configure(EntityTypeBuilder<StaffInvite> b)
        {
            b.ToTable("StaffInvites");
            b.HasKey(x => x.Id);

            b.Property(x => x.Email).HasMaxLength(256).IsRequired();
            b.Property(x => x.Token).HasMaxLength(128).IsRequired();

            b.HasIndex(x => new { x.Email, x.Token }).IsUnique();
        }
    }
}
