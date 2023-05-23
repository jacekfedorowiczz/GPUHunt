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
    public class GraphicCardConfiguration : IEntityTypeConfiguration<Domain.Entities.GraphicCard>
    {
        public void Configure(EntityTypeBuilder<GraphicCard> builder)
        {
            builder.Property(g => g.Vendor)
                .IsRequired();

            builder.Property(g => g.Model)
                .IsRequired();

            builder.Property(g => g.LowestPrice)
                .IsRequired()
                .HasPrecision(7, 2);

            builder.Property(g => g.HighestPrice)
                .HasPrecision(7, 2);
        }
    }
}
