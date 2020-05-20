using SebGlow.Service.Model;
using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace SebGlow.Service
{
    public interface IGitHubStatisticsService
    {
        Task<Maybe<GitHubStatistics>> GetStatistics(string username);
    }
}
