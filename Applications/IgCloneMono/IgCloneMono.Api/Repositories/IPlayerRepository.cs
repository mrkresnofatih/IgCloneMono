using System.Threading.Tasks;
using IgCloneMono.Api.Models;

namespace IgCloneMono.Api.Repositories
{
    public interface IPlayerRepository
    {
        Task<PlayerGetDto> AddOne(Player player);

        Task<PlayerGetDto> GetOne(long playerId);

        Task<Player> GetRawOneByUsername(string username);
    }
}