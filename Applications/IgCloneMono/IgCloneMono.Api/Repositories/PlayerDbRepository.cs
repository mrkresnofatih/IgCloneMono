using System;
using System.Threading.Tasks;
using AutoMapper;
using IgCloneMono.Api.Constants.Exceptions;
using IgCloneMono.Api.Contexts;
using IgCloneMono.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace IgCloneMono.Api.Repositories
{
    public class PlayerDbRepository : IPlayerRepository
    {
        public PlayerDbRepository(IgCloneDbContext igCloneDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _igCloneDbContext = igCloneDbContext;
        }

        private readonly IgCloneDbContext _igCloneDbContext;
        private readonly IMapper _mapper;

        public async Task<PlayerGetDto> AddOne(Player player)
        {
            player.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            await _igCloneDbContext.Players.AddAsync(player);
            await _igCloneDbContext.SaveChangesAsync();
            return _mapper.Map<PlayerGetDto>(player);
        }

        public async Task<PlayerGetDto> GetOne(long playerId)
        {
            var foundPlayer = await _igCloneDbContext
                .Players
                .SingleOrDefaultAsync(p => p.PlayerId == playerId);
            if (foundPlayer == null)
            {
                throw new RecordNotFoundException();
            }

            return _mapper.Map<PlayerGetDto>(foundPlayer);
        }

        public async Task<Player> GetRawOneByUsername(string username)
        {
            var foundPlayer = await _igCloneDbContext
                .Players
                .SingleOrDefaultAsync(p => p.Username == username);
            if (foundPlayer == null)
            {
                throw new RecordNotFoundException();
            }

            return foundPlayer;
        }
    }
}