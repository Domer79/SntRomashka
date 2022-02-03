using Microsoft.AspNetCore.Authorization;
using Snt.Romashka.Contracts;

namespace Snt.Romashka.Host.Authentication
{
    public class CustomSecurityAttribute : AuthorizeAttribute
    {
        public CustomSecurityAttribute(SecurityPolicy securityPolicy)
        {
            Policy = securityPolicy.ToString();
            AuthenticationSchemes = "Token";
        }
    }
}