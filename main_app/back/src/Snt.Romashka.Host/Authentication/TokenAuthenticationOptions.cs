using Microsoft.AspNetCore.Authentication;

namespace Snt.Romashka.Host.Authentication
{
    public class TokenAuthenticationOptions: AuthenticationSchemeOptions
    {
        public TokenAuthenticationOptions()
        {
        }

        public const string SchemeName = "Token";
    }
}