using SebGlow.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SebGlowDbContext : DbContext
    {
        public DbSet<Statistic> Statistics { get; set; }

        public SebGlowDbContext(DbContextOptions<SebGlowDbContext> options)
           : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StatisticConfiguration());
        }
    }
}
