using System;

namespace SebGlow.GitHub.config
{
    public class GitHubConfig
    {
        public Uri BaseAddress { get; set; }
        public string User { get; set; }
        public string Token { get; set; }

        public bool AuthorizedRequest => !(String.IsNullOrWhiteSpace(User) || String.IsNullOrWhiteSpace(Token));
        public string BasicAuthenticationString => getBasicAuthentication();

        private string getBasicAuthentication()
        {
            var basicAuthentication = $"{User}:{Token}";

            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(basicAuthentication);
            return Convert.ToBase64String(textBytes);
        }
    }
}
