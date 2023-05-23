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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.GraphicCard>()
                .OwnsOne(g => g.LowestPriceShop);

            modelBuilder.Entity<Domain.Entities.GraphicCard>()
                .OwnsOne(g => g.HighestPriceShop);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GraphicCardConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
