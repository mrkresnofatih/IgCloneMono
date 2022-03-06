using IgCloneMono.Api.Templates;

namespace IgCloneMono.Api.Utils
{
    public class PlayerPasswordUtils : BcryptTemplate
    {
        protected override string GetSalt()
        {
            return "BPEptQIj2Bhr8dXaUJOfIc5MKWBwMT2bksK41rEv";
        }
    }
}