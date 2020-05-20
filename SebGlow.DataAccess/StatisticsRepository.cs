using System.Linq;
using System.Threading.Tasks;
using SebGlow.Service.Provider;
using CSharpFunctionalExtensions;
using DataAccess;

namespace SebGlow.DataAccess
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private const int VALIDITY_TIME_SECONDS = 3600;

        private IDateTimeProvider dateTimeProvider;
        private SebGlowDbContext dbContext;

        public StatisticsRepository(
            SebGlowDbContext dbContext,
            IDateTimeProvider dateTimeProvider)
        {
            this.dbContext = dbContext;
            this.dateTimeProvider = dateTimeProvider;
        }

        public Maybe<Statistic> GetStatistic(string username)
        {
            var statistics = this.dbContext.Statistics
                .FirstOrDefault(s => 
                    s.createdAt.AddSeconds(VALIDITY_TIME_SECONDS) > dateTimeProvider.Now &&
                    s.owner_login == username);

            return statistics;
        }

        public async Task Save(Statistic statistics)
        {
            statistics.createdAt = dateTimeProvider.Now;

            this.dbContext.Statistics.Add(statistics);
            this.dbContext.SaveChangesAsync();
        }
    }
}
