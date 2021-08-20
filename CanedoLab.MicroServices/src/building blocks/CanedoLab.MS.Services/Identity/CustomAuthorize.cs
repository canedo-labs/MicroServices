using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace CanedoLab.MS.Services.Identity
{
    public class CustomAuthorization
    {
        public static bool ValidateUserClaim(HttpContext context, string claimType, string claimValue) 
        {
            return context.User.Identity.IsAuthenticated
                && context.User.Claims.Any(c => c.Type.Equals(claimType) && c.Value.Equals(claimValue));
        }
    }

    public class ClaimAuthorizeAttribute : TypeFilterAttribute 
    {
        public ClaimAuthorizeAttribute(string claimType, string claimValue) : base(typeof(RequiredClaimFilter))
        {
            Arguments = new object[] { new Claim(claimType.ToLower(), claimValue.ToLower()) };
        }
    }

    public class RequiredClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequiredClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated == false) 
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);

                return;
            }

            if (CustomAuthorization.ValidateUserClaim(context.HttpContext, _claim.Type, _claim.Value) == false) 
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }
}
