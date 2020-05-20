using SebGlow.GitHub;
using SebGlow.GitHub.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SebGlow.Api.fakes
{
    public class FakeRepositoryClient : IRepositoryClient
    {
        public async Task<Maybe<IEnumerable<Repository>>> GetRepositories(string username)
        {
            var r1 = new Repository
            {
                name = "repo1",
                size = 100,
                forks_count = 1,
                stargazers_count = 2,
                watchers_count = 3,
                owner = new Owner
                {
                    id = 1,
                    login = "sebglow",
                    url = "http://github.com/username"
                }
            };

            var r2 = new Repository
            {
                name = "repo2",
                size = 200,
                forks_count = 2,
                stargazers_count = 3,
                watchers_count = 4,
                owner = new Owner
                {
                    id = 1,
                    login = "sebglow",
                    url = "http://github.com/username"
                }
            };

            var r3 = new Repository
            {
                name = "repo3",
                size = 300,
                forks_count = 3,
                stargazers_count = 4,
                watchers_count = 5,
                owner = new Owner
                {
                    id = 1,
                    login = "sebglow",
                    url = "http://github.com/username"
                }
            };

            var result = new List<Repository>
            {
                r1,
                r2,
                r3
            };

            await Task.Delay(1500);

            return result;
        }
    }
}
