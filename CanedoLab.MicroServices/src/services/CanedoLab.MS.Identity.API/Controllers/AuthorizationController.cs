using CanedoLab.MS.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CanedoLab.MS.Identity.API.Controllers
{
    [Route("api/authorization")]
    public class AuthorizationController : ApiController
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthorizationController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("create-claim")]
        public async Task<ActionResult> CreateClaim(AddUserClaimViewModel addUserClaim)
        {
            var identityUser = await _userManager.FindByEmailAsync(addUserClaim.Email);

            var result = await _userManager.AddClaimAsync(identityUser, new Claim(addUserClaim.ValueType.ToLower(), addUserClaim.Value.ToLower()));

            if (result.Succeeded == false) 
            {
                foreach (var error in result.Errors)
                {
                    AddError(error.Description);
                }

                return Response();
            }

            var claims = await GetUserClaimsAndRolesAsync(identityUser);

            return Response(new UserClaimViewModel
            {
                UserId = new Guid(identityUser.Id),
                Email = identityUser.Email,
                UserName = identityUser.UserName,
                PhoneNumber = identityUser.PhoneNumber,
                Claims = claims.Select(c => new UserClaim()
                {
                    Type = c.Type,
                    Value = c.Value
                })
            });
        }

        [HttpGet("claim")]
        public async Task<ActionResult> GetClaim([FromQuery] UserViewModel user) 
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);

            var claims = await GetUserClaimsAndRolesAsync(identityUser);

            return Response(new UserClaimViewModel 
            {
                UserId = new Guid(identityUser.Id),
                Email = identityUser.Email,
                UserName = identityUser.UserName,
                PhoneNumber = identityUser.PhoneNumber,
                Claims = claims.Select(c => new UserClaim()
                {
                    Type = c.Type,
                    Value = c.Value
                })
            });
        }

        private async Task<IList<Claim>> GetUserClaimsAndRolesAsync(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id)); // (Subject) Claim
            claims.Add(new(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); // (JWT ID) Claim
            

            foreach (var userRole in userRoles)
            {
                claims.Add(new("role", userRole));
            }

            return claims;
        }

        private static long ToUnixEpochDate(DateTime dateTime)
        {
            var datetimeOffset = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

            return (long)Math.Round((dateTime.ToUniversalTime() - datetimeOffset).TotalSeconds);
        }
    }
}
