using System.Threading.Tasks;
using SebGlow.DataAccess;
using SebGlow.Service;
using SebGlow.Service.Model;
using AutoMapper;
using CSharpFunctionalExtensions;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace SebGlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoriesController : ControllerBase
    {
        private IStatisticsRepository statisticsRepository;
        private IGitHubStatisticsService statisticsService;
        private IMapper mapper;

        public RepositoriesController(
            IStatisticsRepository statisticsRepository,
            IGitHubStatisticsService statisticsService,
            IMapper mapper)
        {
            this.statisticsRepository = statisticsRepository;
            this.statisticsService = statisticsService;
            this.mapper = mapper;
        }
        // GET api/repositories/{owner}
        [HttpGet("{owner}")]
        public async Task<IActionResult> Get(string owner)
        {
            Maybe<GitHubStatistics> githubStatistics;
            var persistedStatistics = statisticsRepository.GetStatistic(owner);
            if (persistedStatistics.HasNoValue)
            {
                githubStatistics = await statisticsService.GetStatistics(owner);
                if (githubStatistics.HasNoValue)
                {
                    return NotFound();
                }

                persistedStatistics = mapper.Map<Statistic>(githubStatistics.Value);
                await statisticsRepository.Save(persistedStatistics.Value);
            }
            else
            {
                githubStatistics = mapper.Map<GitHubStatistics>(persistedStatistics.Value);
            }

            return Ok(githubStatistics.Value);
        }
    }
}