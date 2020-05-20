using SebGlow.DataAccess;
using SebGlow.Service;
using SebGlow.Service.Model;
using AutoMapper;
using DataAccess;
using Moq;
using System;
using System.Collections.Generic;

namespace SebGlow.Api.Tests
{
    public class RepositoryControllerFixture
    {
        public Mock<IStatisticsRepository> StatisticsRepositoryMock;
        public Mock<IGitHubStatisticsService> StatisticsServiceMock;
        public Mock<IMapper> mapperMock;

        public string Owner = "owner_with_statistics_in_db";
        public string OwnerWithNoStatisticsInDB = "owner_with_no_statistics_in_db";
        public string UserWithNoRepo = "no_repo_user";

        public Statistic DatabaseStatistics = new Statistic
        {
            id = 1,
            avgForks = 1,
            avgStargazers = 2,
            avgSize = 100,
            avgWatchers = 10,
            createdAt = new DateTime(2020, 4, 20),
            letters = new KeyValuePair<char, int>[] { },
            owner_id = 1,
            owner_login = "owner_name",
            owner_url = "https://"
        };

        public GitHubStatistics RetrievedGitHubStatistics = new GitHubStatistics
        {
            avgForks = 1,
            avgSize = 100,
            avgStargazers = 2,
            avgWatchers = 10,
            letters = new Letters(),
            owner = new RepositoriesOwner
            {
                id = 1,
                login = "owner_name",
                url = "https://"
            }
        };

        public GitHubStatistics StoredGitHubStatistics = new GitHubStatistics
        {
            avgForks = 1,
            avgSize = 100,
            avgStargazers = 2,
            avgWatchers = 9,
            letters = new Letters(),
            owner = new RepositoriesOwner
            {
                id = 1,
                login = "owner_name",
                url = "https://"
            }
        };


        public RepositoryControllerFixture()
        {
            StatisticsRepositoryMock = new Mock<IStatisticsRepository>();
            StatisticsServiceMock = new Mock<IGitHubStatisticsService>();
            mapperMock = new Mock<IMapper>();

            StatisticsRepositoryMock.Setup(r => r.GetStatistic(UserWithNoRepo))
                .Returns(() => null);
            StatisticsRepositoryMock.Setup(r => r.GetStatistic(OwnerWithNoStatisticsInDB))
                .Returns(() => null);
            StatisticsRepositoryMock.Setup(r => r.GetStatistic(Owner))
                .Returns(() => DatabaseStatistics);

            StatisticsServiceMock.Setup(s => s.GetStatistics(UserWithNoRepo))
                .ReturnsAsync(() => null);
            StatisticsServiceMock.Setup(s => s.GetStatistics(OwnerWithNoStatisticsInDB))
                .ReturnsAsync(() => RetrievedGitHubStatistics);

            mapperMock.Setup(m => m.Map<GitHubStatistics>(DatabaseStatistics))
                .Returns(() => StoredGitHubStatistics);
            mapperMock.Setup(m => m.Map<Statistic>(RetrievedGitHubStatistics))
                .Returns(() => DatabaseStatistics);
        }
    }
}
