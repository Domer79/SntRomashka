using Microsoft.AspNetCore.Authorization;

namespace Snt.Romashka.Host.Authentication
{
    public class PolicyAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Policy { get; }

        public PolicyAuthorizationRequirement(string policy)
        {
            Policy = policy;
        }
    }
}