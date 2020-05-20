using SebGlow.GitHub;
using SebGlow.GitHub.Tests;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Should_Call_Valid_GitHub_Api_AddressAsync()
        {
            var fixture = new GitHubClientFixture();

            var sut = new GitHubClient(fixture.HttpClient, fixture.GitHubConfig);

            await sut.GetRepositories(fixture.Owner);
            
            fixture.HandlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req =>
                 req.Method == HttpMethod.Get &&
                 req.RequestUri == fixture.ValidApiUrl(fixture.Owner)),
               ItExpr.IsAny<CancellationToken>()
               );
        }

        [Test]
        public async Task Should_Return_Valid_Response_If_User_Has_Repos()
        {
            var fixture = new GitHubClientFixture();

            var sut = new GitHubClient(fixture.HttpClient, fixture.GitHubConfig);

            var response = await sut.GetRepositories(fixture.Owner);

            Assert.IsTrue(response.HasValue);
            Assert.AreEqual(1, response.Value.Count());
            Assert.AreEqual(fixture.Repository, response.Value.First());
        }

        [Test]
        public async Task Should_Return_No_Value_If_User_Has_No_Repos()
        {
            var fixture = new GitHubClientFixture();

            var sut = new GitHubClient(fixture.HttpClient, fixture.GitHubConfig);

            var response = await sut.GetRepositories(fixture.UserWithNoRepo);

            Assert.IsFalse(response.HasValue);
        }

        [Test]
        public async Task Should_Return_No_Value_If_HttpClient_Throws_Exception()
        {
            var fixture = new GitHubClientFixture();

            var sut = new GitHubClient(fixture.HttpClient, fixture.GitHubConfig);

            var response = await sut.GetRepositories(fixture.HttpFailureUser);

            Assert.IsFalse(response.HasValue);
        }
    }
}