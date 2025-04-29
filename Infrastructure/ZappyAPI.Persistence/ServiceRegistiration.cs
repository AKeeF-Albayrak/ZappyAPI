using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Persistence.Contexts;

namespace ZappyAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<ZappyAPIDbContextFactory>()
                .Build();

            services.AddDbContext<ZappyAPIDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Scoped
            );
        }
    }
}
