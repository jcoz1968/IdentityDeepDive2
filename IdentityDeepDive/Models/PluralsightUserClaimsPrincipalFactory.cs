﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityDeepDive.Models
{
    public class PluralsightUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<PluralsightUser>
    {
        private UserManager<PluralsightUser> _userManager;

        public PluralsightUserClaimsPrincipalFactory(UserManager<PluralsightUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(PluralsightUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("locale", user.Locale));
            return identity;
        }
    }
}
