using System.Collections.Generic;
using System.Threading.Tasks;
using IgCloneMono.Api.Models;

namespace IgCloneMono.Api.Repositories
{
    public interface IPlayerDbRepository
    {
        Task<PlayerGetDto> AddOne(Player player);

        Task<PlayerGetDto> GetOne(long playerId);

        Task<Dictionary<long, PlayerGetDto>> GetMany(List<long> playerIds);

        Task<Player> GetRawOneByUsername(string username);
    }
}