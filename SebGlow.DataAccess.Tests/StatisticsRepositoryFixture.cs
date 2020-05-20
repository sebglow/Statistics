using SebGlow.Service.Provider;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;

namespace SebGlow.DataAccess.Tests
{
    public class StatisticsRepositoryFixture
    {
        Mock<DbSet<Statistic>> StatisticsMock = new Mock<DbSet<Statistic>>();

        public string RepositoryOwner = "owner";

        public Mock<IDateTimeProvider> DateTimeProviderNow = new Mock<IDateTimeProvider>();
        public Mock<IDateTimeProvider> DateTimeProviderFuture = new Mock<IDateTimeProvider>();

        public DbContextOptions<SebGlowDbContext> Options = new DbContextOptionsBuilder<SebGlowDbContext>()
                .UseInMemoryDatabase(databaseName: "SebGlowDb")
                .Options;

        public Statistic storedStatistics;

        public StatisticsRepositoryFixture()
        {
            DateTimeProviderNow.Setup(p => p.Now)
                .Returns(() => new DateTime(2020, 4, 20, 12, 0, 1));

            DateTimeProviderFuture.Setup(p => p.Now)
                .Returns(() => new DateTime(2020, 5, 20, 12, 0, 1));

            storedStatistics = new Statistic
            {
                avgForks = 1,
                avgSize = 2,
                avgStargazers = 3,
                avgWatchers = 4,
                id = 1,
                letters = new KeyValuePair<char, int>[] { },
                owner_id = 1,
                owner_login = RepositoryOwner,
                owner_url = "https://",
                createdAt = new DateTime(2020, 4, 20, 12, 0, 0),
            };

            using (var context = new SebGlowDbContext(Options))
            {
                context.Statistics.Add(storedStatistics);

                context.SaveChanges();
            }
        }
    }
}
