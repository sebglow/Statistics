using SebGlow.GitHub.config;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SebGlow.GitHub.Model;

namespace SebGlow.GitHub.Tests
{
    public class GitHubClientFixture
    {
        public string Owner = "github_repository_owner";
        public string UserWithNoRepo = "no_repo_user";
        public string HttpFailureUser = "http_failure_user";

        public Uri ValidApiUrl(string user) => new Uri($"https://api.github.com/users/{user}/repos");

        public Mock<HttpMessageHandler> HandlerMock;
        public HttpClient HttpClient;
        public IOptions<GitHubConfig> GitHubConfig;
               
        public GitHubClientFixture()
        {
            HandlerMock = new Mock<HttpMessageHandler>();
            HandlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.LocalPath.Contains(Owner)),
                    ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(HttpValidResponse)
               .Verifiable();

            HandlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.LocalPath.Contains(UserWithNoRepo)),
                    ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(HttpEmptyResponse)
               .Verifiable();

            HandlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.LocalPath.Contains(HttpFailureUser)),
                    ItExpr.IsAny<CancellationToken>())
               .Throws(new HttpRequestException())
               .Verifiable();

            HttpClient = new HttpClient(HandlerMock.Object);

            GitHubConfig = Options.Create(new GitHubConfig
            {
                BaseAddress = new Uri("https://api.github.com"),
                User = "user",
                Token = "very_long_authorization_token"
            });
        }

        public Repository Repository = new Repository
        {
            forks_count = 0,
            stargazers_count = 0,
            watchers_count = 0,
            size = 137,
            name = "react-redux-typescript-pluralsight",
            owner = new Owner
            {
                id = 5710639,
                login = "sebglow",
                url = "https://api.github.com/users/sebglow"
            }
        };

        public HttpResponseMessage HttpValidResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(@"[
              {
                ""id"": 175832481,
                ""node_id"": ""MDEwOlJlcG9zaXRvcnkxNzU4MzI0ODE="",
                ""name"": ""react-redux-typescript-pluralsight"",
                ""full_name"": ""sebglow/react-redux-typescript-pluralsight"",
                ""private"": false,
                ""owner"": {
                  ""login"": ""sebglow"",
                  ""id"": 5710639,
                  ""node_id"": ""MDQ6VXNlcjU3MTA2Mzk="",
                  ""avatar_url"": ""https://avatars2.githubusercontent.com/u/5710639?v=4"",
                  ""gravatar_id"": """",
                  ""url"": ""https://api.github.com/users/sebglow"",
                  ""html_url"": ""https://github.com/sebglow"",
                  ""followers_url"": ""https://api.github.com/users/sebglow/followers"",
                  ""following_url"": ""https://api.github.com/users/sebglow/following{/other_user}"",
                  ""gists_url"": ""https://api.github.com/users/sebglow/gists{/gist_id}"",
                  ""starred_url"": ""https://api.github.com/users/sebglow/starred{/owner}{/repo}"",
                  ""subscriptions_url"": ""https://api.github.com/users/sebglow/subscriptions"",
                  ""organizations_url"": ""https://api.github.com/users/sebglow/orgs"",
                  ""repos_url"": ""https://api.github.com/users/sebglow/repos"",
                  ""events_url"": ""https://api.github.com/users/sebglow/events{/privacy}"",
                  ""received_events_url"": ""https://api.github.com/users/sebglow/received_events"",
                  ""type"": ""User"",
                  ""site_admin"": false
                },
                ""html_url"": ""https://github.com/sebglow/react-redux-typescript-pluralsight"",
                ""description"": ""Based on pluralsight course"",
                ""fork"": true,
                ""url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight"",
                ""forks_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/forks"",
                ""keys_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/keys{/key_id}"",
                ""collaborators_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/collaborators{/collaborator}"",
                ""teams_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/teams"",
                ""hooks_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/hooks"",
                ""issue_events_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/issues/events{/number}"",
                ""events_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/events"",
                ""assignees_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/assignees{/user}"",
                ""branches_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/branches{/branch}"",
                ""tags_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/tags"",
                ""blobs_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/git/blobs{/sha}"",
                ""git_tags_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/git/tags{/sha}"",
                ""git_refs_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/git/refs{/sha}"",
                ""trees_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/git/trees{/sha}"",
                ""statuses_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/statuses/{sha}"",
                ""languages_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/languages"",
                ""stargazers_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/stargazers"",
                ""contributors_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/contributors"",
                ""subscribers_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/subscribers"",
                ""subscription_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/subscription"",
                ""commits_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/commits{/sha}"",
                ""git_commits_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/git/commits{/sha}"",
                ""comments_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/comments{/number}"",
                ""issue_comment_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/issues/comments{/number}"",
                ""contents_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/contents/{+path}"",
                ""compare_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/compare/{base}...{head}"",
                ""merges_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/merges"",
                ""archive_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/{archive_format}{/ref}"",
                ""downloads_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/downloads"",
                ""issues_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/issues{/number}"",
                ""pulls_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/pulls{/number}"",
                ""milestones_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/milestones{/number}"",
                ""notifications_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/notifications{?since,all,participating}"",
                ""labels_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/labels{/name}"",
                ""releases_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/releases{/id}"",
                ""deployments_url"": ""https://api.github.com/repos/sebglow/react-redux-typescript-pluralsight/deployments"",
                ""created_at"": ""2019-03-15T14:10:35Z"",
                ""updated_at"": ""2019-03-15T14:10:37Z"",
                ""pushed_at"": ""2017-04-13T11:56:55Z"",
                ""git_url"": ""git://github.com/sebglow/react-redux-typescript-pluralsight.git"",
                ""ssh_url"": ""git@github.com:sebglow/react-redux-typescript-pluralsight.git"",
                ""clone_url"": ""https://github.com/sebglow/react-redux-typescript-pluralsight.git"",
                ""svn_url"": ""https://github.com/sebglow/react-redux-typescript-pluralsight"",
                ""homepage"": null,
                ""size"": 137,
                ""stargazers_count"": 0,
                ""watchers_count"": 0,
                ""language"": ""TypeScript"",
                ""has_issues"": false,
                ""has_projects"": true,
                ""has_downloads"": true,
                ""has_wiki"": true,
                ""has_pages"": false,
                ""forks_count"": 0,
                ""mirror_url"": null,
                ""archived"": false,
                ""disabled"": false,
                ""open_issues_count"": 0,
                ""license"": null,
                ""forks"": 0,
                ""open_issues"": 0,
                ""watchers"": 0,
                ""default_branch"": ""master""
              }
            ]
            "),
        };


        public HttpResponseMessage HttpEmptyResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("[ ]"),
        };
    }
}
