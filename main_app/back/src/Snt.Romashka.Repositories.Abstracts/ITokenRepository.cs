using System.Threading.Tasks;
using Snt.Romashka.Contracts;

namespace Snt.Romashka.Repositories.Abstracts
{
    public interface ITokenRepository
    {
        Task<Token> GetTokenById(string tokenId);
        Task<Token> Add(Token token);
    }
}