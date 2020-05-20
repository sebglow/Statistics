using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace SebGlow.Service.Tests
{
    public class GitHubStatisticsServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Should_Call_GitHubClient_For_GitHub_Repositories()
        {
            var fixture = new GitHubStatisticsServiceFixture();

            var sut = new GitHubStatisticsService(fixture.GitHubClietnMock.Object);

            await sut.GetStatistics(fixture.Owner);

            fixture.GitHubClietnMock.Verify(r => r.GetRepositories(fixture.Owner), Times.Once);
        }

        [Test]
        public async Task Should_Return_Valid_Statistic_For_Repositories()
        {
            var fixture = new GitHubStatisticsServiceFixture();

            var sut = new GitHubStatisticsService(fixture.GitHubClietnMock.Object);

            var result = await sut.GetStatistics(fixture.Owner);

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(fixture.CalculatedStatistics, result.Value);
        }

        [Test]
        public async Task Should_Return_No_Value_If_User_Has_No_Repositories()
        {
            var fixture = new GitHubStatisticsServiceFixture();

            var sut = new GitHubStatisticsService(fixture.GitHubClietnMock.Object);

            var result = await sut.GetStatistics(fixture.UserWithNoRepo);

            Assert.IsFalse(result.HasValue);
        }
    }
}