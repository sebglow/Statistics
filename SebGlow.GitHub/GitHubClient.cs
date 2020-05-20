using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SebGlow.GitHub.config;
using SebGlow.GitHub.Model;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;

namespace SebGlow.GitHub
{
    public class GitHubClient : IRepositoryClient
    {
        private HttpClient client;
        private string userRepositoriesQuery(string username) => $"/users/{username}/repos";

        public GitHubClient(HttpClient client, IOptions<GitHubConfig> config)
        {
            this.client = client;
            this.client.DefaultRequestHeaders.Add("User-Agent", "C# App");
            this.client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");

            this.client.BaseAddress = config.Value.BaseAddress;
            if (config.Value.AuthorizedRequest)
            {
                this.client.DefaultRequestHeaders.Add("Authorization", $"Basic {config.Value.BasicAuthenticationString}");
            }
        }
        
        public async Task<Maybe<IEnumerable<Repository>>> GetRepositories(string username)
        {
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(userRepositoriesQuery(username));
            }
            catch (HttpRequestException ex)
            {
                return null;
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var repositories = JsonSerializer.Deserialize<IEnumerable<Repository>>(responseString);

            if (repositories.Any())
            {
                return repositories.ToList();
            }

            return null;
        }
    }
}
