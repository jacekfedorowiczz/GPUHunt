using GPUHunt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Infrastructure.Configuration
{
    public class VendorConfiguration : IEntityTypeConfiguration<Domain.Entities.Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.Property(v => v.Name)
                .IsRequired();
        }
    }
}
