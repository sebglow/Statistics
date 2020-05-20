using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SebGlowGitApi
{
    public class DbContextFactory : IDesignTimeDbContextFactory<SebGlowDbContext>
    {
        public SebGlowDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var builder = new DbContextOptionsBuilder<SebGlowDbContext>();

            var connectionString = configuration.GetConnectionString("SebGlowDb");

            builder.UseSqlServer(connectionString);

            return new SebGlowDbContext(builder.Options);
        }
    }
}
