using SebGlow.GitHub;
using SebGlow.GitHub.Model;
using SebGlow.Service.Model;
using CSharpFunctionalExtensions;
using Moq;
using System.Collections.Generic;

namespace SebGlow.Service.Tests
{
    public class GitHubStatisticsServiceFixture
    {
        public string Owner = "github_repository_owner";
        public string UserWithNoRepo = "no_repo_user";


        public GitHubStatistics CalculatedStatistics = new GitHubStatistics()
        {
            avgForks = 2,
            avgSize = 200,
            avgStargazers = 3,
            avgWatchers = 4,
            letters = new Letters(new KeyValuePair<char, int>[] {
                new KeyValuePair<char, int>('1', 1 ),
                new KeyValuePair<char, int>('2', 1 ),
                new KeyValuePair<char, int>('3', 1 ),
                new KeyValuePair<char, int>('e', 3 ),
                new KeyValuePair<char, int>('o', 3 ),
                new KeyValuePair<char, int>('p', 3 ),
                new KeyValuePair<char, int>('r', 3 )
            }),
            owner = new RepositoriesOwner
            {
                id = 1,
                login = "github_repository_owner",
                url = "http://github.com/username",
            },
        };

        public Maybe<IEnumerable<Repository>> Repositories = new List<Repository>
        {
            new Repository
            {
                name = "repo1",
                size = 100,
                forks_count = 1,
                stargazers_count = 2,
                watchers_count = 3,
                owner = new Owner
                {
                    id = 1,
                    login = "github_repository_owner",
                    url = "http://github.com/username"
                }
            },
            new Repository
            {
                name = "repo2",
                size = 200,
                forks_count = 2,
                stargazers_count = 3,
                watchers_count = 4,
                owner = new Owner
                {
                    id = 1,
                    login = "github_repository_owner",
                    url = "http://github.com/username"
                }
            },
            new Repository
            {
                name = "repo3",
                size = 300,
                forks_count = 3,
                stargazers_count = 4,
                watchers_count = 5,
                owner = new Owner
                {
                    id = 1,
                    login = "github_repository_owner",
                    url = "http://github.com/username"
                }
            }
        };

        public Mock<IRepositoryClient> GitHubClietnMock;

        public GitHubStatisticsServiceFixture()
        {
            GitHubClietnMock = new Mock<IRepositoryClient>();
            GitHubClietnMock.Setup(c => c.GetRepositories(Owner))
                .ReturnsAsync(() => Repositories);
        }

        
    }
}
