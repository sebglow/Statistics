using CSharpFunctionalExtensions;
using DataAccess;
using System.Threading.Tasks;

namespace SebGlow.DataAccess
{
    public interface IStatisticsRepository
    {
        Maybe<Statistic> GetStatistic(string username);
        Task Save(Statistic statistics);
    }
}
