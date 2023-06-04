using GPUHunt.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Infrastructure.Persistence
{
    public class GPUHuntDbContext : IdentityDbContext
    {
        public GPUHuntDbContext(DbContextOptions<GPUHuntDbContext> options) : base(options)
        {
            
        }

        public DbSet<Domain.Entities.GraphicCard> GraphicCards { get; set; }
        public DbSet<Domain.Entities.Vendor> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.GraphicCard>()
                .HasOne(g => g.Vendor)
                .WithMany(v => v.GraphicCards)
                .HasForeignKey(g => g.VendorId);

            modelBuilder.Entity<Domain.Entities.GraphicCard>()
                .OwnsOne(g => g.LowestPriceStore);

            modelBuilder.Entity<Domain.Entities.GraphicCard>()
                .OwnsOne(g => g.HighestPriceStore);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GraphicCardConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
