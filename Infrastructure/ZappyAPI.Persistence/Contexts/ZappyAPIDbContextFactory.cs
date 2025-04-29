using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Persistence.Contexts
{
    public class ZappyAPIDbContextFactory : IDesignTimeDbContextFactory<ZappyAPIDbContext>
    {
        public ZappyAPIDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<ZappyAPIDbContextFactory>()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ZappyAPIDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new ZappyAPIDbContext(optionsBuilder.Options);
        }
    }
}
