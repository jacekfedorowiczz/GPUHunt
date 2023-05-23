using GPUHunt.Domain.Interfaces;
using GPUHunt.Infrastructure.Persistence;
using GPUHunt.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GPUHuntDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("LocalDb"));
            });

            services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<GPUHuntDbContext>();

            services.AddScoped<IGraphicCardRepository, GraphicCardRepository>();
        }
    }
}
