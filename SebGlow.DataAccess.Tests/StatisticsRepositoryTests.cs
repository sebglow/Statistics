using DataAccess;
using NUnit.Framework;

namespace SebGlow.DataAccess.Tests
{
    public class StatisticsRepositoryTests
    {
        private StatisticsRepositoryFixture fixture = new StatisticsRepositoryFixture();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_Return_Statistic_Object_Younger_Than_One_Hour()
        {
            using (var dbContext = new SebGlowDbContext(fixture.Options))
            {
                var sut = new StatisticsRepository(dbContext, fixture.DateTimeProviderNow.Object);
                var result = sut.GetStatistic(fixture.RepositoryOwner);

                Assert.IsTrue(result.HasValue);
            }
        }

        [Test]
        public void Should_Return_No_Value_If_No_Record_Younger_Than_One_Hour()
        {
            using (var dbContext = new SebGlowDbContext(fixture.Options))
            {
                var sut = new StatisticsRepository(dbContext, fixture.DateTimeProviderFuture.Object);
                var result = sut.GetStatistic(fixture.RepositoryOwner);

                Assert.IsFalse(result.HasValue);
            }
        }
    }
}