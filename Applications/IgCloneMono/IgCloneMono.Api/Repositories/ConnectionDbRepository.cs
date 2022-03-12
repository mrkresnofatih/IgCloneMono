using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IgCloneMono.Api.Contexts;
using IgCloneMono.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace IgCloneMono.Api.Repositories
{
    public class ConnectionDbRepository : IConnectionDbRepository
    {
        public ConnectionDbRepository(IgCloneDbContext igCloneDbContext)
        {
            _igCloneDbContext = igCloneDbContext;
        }

        private readonly IgCloneDbContext _igCloneDbContext;
        
        public async Task<Connection> AddOne(Connection connection)
        {
            connection.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            await _igCloneDbContext.Connections.AddAsync(connection);
            await _igCloneDbContext.SaveChangesAsync();
            return connection;
        }

        public async Task<Connection> GetOneByPlayerIdAndFollowerId(long playerId, long followerId)
        {
            return await _igCloneDbContext
                .Connections
                .SingleOrDefaultAsync(p => (p.PlayerId == playerId && p.FollowerId == followerId));
        }

        public async Task<Connection> DeleteOneByConnectionId(long connectionId)
        {
            var conn = await _igCloneDbContext.Connections.FindAsync(connectionId);
            conn.Deleted = true;
            await _igCloneDbContext.SaveChangesAsync();
            return conn;
        }

        public async Task<Connection> UndeleteOneByConnectionId(long connectionId)
        {
            var conn = await _igCloneDbContext.Connections.FindAsync(connectionId);
            conn.Deleted = false;
            await _igCloneDbContext.SaveChangesAsync();
            return conn;
        }

        public async Task<Dictionary<long, bool>> GetFollowerIdList(long playerId)
        {
            return await _igCloneDbContext
                .Connections
                .Where(p => (p.PlayerId == playerId && !p.Deleted))
                .ToDictionaryAsync(p => p.FollowerId, _ => true);
        }

        public async Task<Dictionary<long, bool>> GetFollowingIdList(long playerId)
        {
            return await _igCloneDbContext
                .Connections
                .Where(p => (p.FollowerId == playerId && !p.Deleted))
                .ToDictionaryAsync(p => p.PlayerId, _ => true);
        }
    }
}