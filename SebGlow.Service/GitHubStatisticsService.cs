using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SebGlow.GitHub;
using SebGlow.GitHub.Model;
using SebGlow.Service.Model;
using CSharpFunctionalExtensions;

namespace SebGlow.Service
{
    public class GitHubStatisticsService : IGitHubStatisticsService
    {
        private IRepositoryClient gitHubClietn;

        public GitHubStatisticsService(
            IRepositoryClient gitHubClietn)
        {
            this.gitHubClietn = gitHubClietn;
        }

        public async Task<Maybe<GitHubStatistics>> GetStatistics(string username)
        {
            var gitHubRepositories = await gitHubClietn.GetRepositories(username);
            if (gitHubRepositories.HasNoValue)
            {
                return null;
            }

            var newStatistics = calculate(gitHubRepositories.Value);

            return newStatistics;
        }

        private GitHubStatistics calculate(IEnumerable<Repository> gitHubRepository)
        {
            var result = new GitHubStatistics();

            var repositoryOwner = new RepositoriesOwner();
            repositoryOwner.id = gitHubRepository.FirstOrDefault().owner.id;
            repositoryOwner.login = gitHubRepository.FirstOrDefault().owner.login;
            repositoryOwner.url = gitHubRepository.FirstOrDefault().owner.url;
            result.owner = repositoryOwner;

            int total = gitHubRepository.Count();
            int stargazersTotal = 0;
            int watchersTotal = 0;
            int forksTotal = 0;
            int sizeTotal = 0;

            string namesConcatenated = string.Empty;

            foreach (var repository in gitHubRepository)
            {
                stargazersTotal += repository.stargazers_count;
                watchersTotal += repository.watchers_count;
                forksTotal += repository.forks_count;
                sizeTotal += repository.size;
                
                namesConcatenated += repository.name.ToLowerInvariant();
            }

            result.avgStargazers = stargazersTotal / total;
            result.avgWatchers = watchersTotal / total;
            result.avgForks = forksTotal / total;
            result.avgSize = sizeTotal / total;

            result.letters = (Letters)namesConcatenated.GroupBy(x => x)
                .OrderBy(x => x.Key)
                .Select(x => new KeyValuePair<char, int>(x.Key, x.Count()))
                .ToArray();

            return result;
        }
    }
}
