﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Snt.Romashka.Contracts;
using Snt.Romashka.Services.Abstracts;

namespace Snt.Romashka.Host.Authentication
{
    public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationOptions>
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public TokenAuthenticationHandler(IOptionsMonitor<TokenAuthenticationOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock,
            IAuthService authService, ITokenService tokenService, 
            IUserService userService) : base(options, logger, encoder, clock)
        {
            _authService = authService;
            _tokenService = tokenService;
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var tokenId = Request.Headers["token"];
            if (string.IsNullOrEmpty(tokenId))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }
            
            try
            {
                if (!await _authService.ValidateToken(tokenId))
                {
                    return AuthenticateResult.Fail("Unauthorized");
                }

                var user = await _authService.GetUserByToken(tokenId);
                var claims = new List<Claim>()
                {
                    new(ClaimTypes.NameIdentifier, user.Login),
                    new(CustomClaimTypes.Token, tokenId),
                    new(CustomClaimTypes.Login, user.Login),
                    new(CustomClaimTypes.UserId, user.Id.ToString())
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new System.Security.Principal.GenericPrincipal(identity, null);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }
    }
}