using SebGlow.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace SebGlow.Api.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Should_Check_For_Statistic_In_Database()
        {
            var fixture = new RepositoryControllerFixture();

            var sut = new RepositoriesController(
                fixture.StatisticsRepositoryMock.Object,
                fixture.StatisticsServiceMock.Object,
                fixture.mapperMock.Object);

            await sut.Get(fixture.Owner);

            fixture.StatisticsRepositoryMock.Verify(r => r.GetStatistic(fixture.Owner), Times.Once);
        }

        [Test]
        public async Task Should_Call_GitHub_For_Statistics_If_None_In_Database()
        {
            var fixture = new RepositoryControllerFixture();

            var sut = new RepositoriesController(
                fixture.StatisticsRepositoryMock.Object,
                fixture.StatisticsServiceMock.Object,
                fixture.mapperMock.Object);

            await sut.Get(fixture.OwnerWithNoStatisticsInDB);

            fixture.StatisticsServiceMock.Verify(r => r.GetStatistics(fixture.OwnerWithNoStatisticsInDB), Times.Once);
        }

        [Test]
        public async Task Should_Not_Call_GitHub_If_Statistics_Exists_In_Database()
        {
            var fixture = new RepositoryControllerFixture();

            var sut = new RepositoriesController(
                fixture.StatisticsRepositoryMock.Object,
                fixture.StatisticsServiceMock.Object,
                fixture.mapperMock.Object);

            await sut.Get(fixture.Owner);

            fixture.StatisticsServiceMock.Verify(r => r.GetStatistics(fixture.Owner), Times.Never);
        }

        [Test]
        public async Task Should_Return_NotFound_If_No_Statistics_Available()
        {
            var fixture = new RepositoryControllerFixture();

            var sut = new RepositoriesController(
                fixture.StatisticsRepositoryMock.Object,
                fixture.StatisticsServiceMock.Object,
                fixture.mapperMock.Object);

            var result = await sut.Get(fixture.UserWithNoRepo);

            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public async Task Should_Return_Preserved_Statistics_If_Exists()
        {
            var fixture = new RepositoryControllerFixture();

            var sut = new RepositoriesController(
                fixture.StatisticsRepositoryMock.Object,
                fixture.StatisticsServiceMock.Object,
                fixture.mapperMock.Object);

            var result = await sut.Get(fixture.Owner) as OkObjectResult;

            Assert.AreSame(fixture.StoredGitHubStatistics, result.Value);
        }

        [Test]
        public async Task Should_Return_Fresh_Statistics_If_None_In_Database()
        {
            var fixture = new RepositoryControllerFixture();

            var sut = new RepositoriesController(
                fixture.StatisticsRepositoryMock.Object,
                fixture.StatisticsServiceMock.Object,
                fixture.mapperMock.Object);

            var result = await sut.Get(fixture.OwnerWithNoStatisticsInDB) as OkObjectResult;

            Assert.AreSame(fixture.RetrievedGitHubStatistics, result.Value);
        }
    }
}