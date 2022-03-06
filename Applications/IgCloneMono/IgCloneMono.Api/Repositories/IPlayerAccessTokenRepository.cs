using System.Threading.Tasks;

namespace IgCloneMono.Api.Repositories
{
    public interface IPlayerAccessTokenRepository
    {
        Task SaveOrExtendAccessToken(string id, string token);

        Task<string> RetrieveStoredAccessToken(string id);

        Task RevokeAccessToken(string id);
    }
}