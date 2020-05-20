using SebGlow.GitHub.Model;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SebGlow.GitHub
{
    public interface IRepositoryClient
    {
        Task<Maybe<IEnumerable<Repository>>> GetRepositories(string username);
    }
}