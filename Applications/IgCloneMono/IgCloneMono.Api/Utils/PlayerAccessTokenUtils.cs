using System;
using System.Collections.Generic;
using IgCloneMono.Api.Templates;

namespace IgCloneMono.Api.Utils
{
    public class PlayerAccessTokenUtils : JwtTemplate
    {
        protected override string GetSecret()
        {
            return "92AG2zPz9ENLkRBgJJNaKxxSgGiL82gRElpeQCpW";
        }
        
        public string GenerateToken(long playerId)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var payload = new Dictionary<string, string>();
            payload.Add("playerId", playerId.ToString());
            payload.Add("genesisTime", now.ToString());
            return GetToken(payload);
        }

        public long GetPlayerIdFromToken(string token)
        {
            var claims = GetPayload(token);
            var playerIdString = claims["playerId"];
            return long.Parse(playerIdString);
        }
    }
}