using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Snt.Romashka.Contracts;
using Snt.Romashka.Services.Abstracts;

namespace Snt.Romashka.Host.Authentication
{
    // TODO: Придумать кэш для хранения
    public class CustomAuthorizationHandler: AuthorizationHandler<PolicyAuthorizationRequirement>
    {
        private readonly IAuthService _authService;

        public CustomAuthorizationHandler(IAuthService authService)
        {
            _authService = authService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PolicyAuthorizationRequirement requirement)
        {
            try
            {
                var securityPolicy = Enum.Parse<SecurityPolicy>(requirement.Policy);
                var userGuid = context.User.Claims.FirstOrDefault(_ => _.Type == CustomClaimTypes.UserId)?.Value;

                if (string.IsNullOrEmpty(userGuid))
                {
                    context.Fail();
                    return;
                }

                var userId = Guid.Parse(userGuid);
                if (!await _authService.CheckAccess(securityPolicy, userId))
                {
                    context.Fail();
                    return;
                }
                
                context.Succeed(requirement);
            }
            catch (Exception ex)
            {
                context.Fail();
            }
        }
    }
}