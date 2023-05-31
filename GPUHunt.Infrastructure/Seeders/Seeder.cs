using GPUHunt.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Infrastructure.Seeders
{
    public class Seeder
    {
        private readonly GPUHuntDbContext _dbContext;

        public Seeder(GPUHuntDbContext dbContext)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task SeedDatabase()
        {
            try
            {
                if (await _dbContext.Database.CanConnectAsync()) 
                {
                    if (!_dbContext.Vendors.Any())
                    {
                        var vendors = await GetVendors();
                        await _dbContext.Vendors.AddRangeAsync(vendors);
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (ArgumentNullException)
            {

                throw;
            }
        }

        private async static Task<List<Domain.Entities.Vendor>> GetVendors()
        {
            var vendors = new List<Domain.Entities.Vendor>()
            {
                new Domain.Entities.Vendor()
                {
                    Name = "NVIDIA"
                },
                new Domain.Entities.Vendor()
                {
                    Name = "AMD"
                },
                new Domain.Entities.Vendor()
                {
                    Name = "Intel"
                },
                new Domain.Entities.Vendor()
                {
                    Name = "Undefinied"
                }
            };

            return vendors;
        }
    }
}
