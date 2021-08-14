using CanedoLab.MS.Identity.API.Extensions;
using CanedoLab.MS.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CanedoLab.MS.Identity.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : ApiController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettingsOption)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettingsOption.Value;
        }

        [HttpPost("register")]
        public async Task<ActionResult> SignUp(UserRegistrationViewModel userRegistration) 
        {
            if (ModelState.IsValid == false)
            {
                return CreateResponse(ModelState);
            }

            var user = new IdentityUser
            {
                UserName = userRegistration.Email,
                Email = userRegistration.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRegistration.Password);

            if (result.Succeeded == false)
            {
                foreach (var identityError in result.Errors)
                {
                    AddError(identityError.Description);
                }

                return CreateResponse();
            }

            return CreateResponse(await CreateUserAuth(userRegistration.Email));
        }

        [HttpPost("login")]
        public async Task<ActionResult> SignIn(UserLoginViewModel userLogin) 
        {
            if (ModelState.IsValid == false)
            {
                return CreateResponse(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email,
                                                                  userLogin.Password,
                                                                  isPersistent: false,
                                                                  lockoutOnFailure: true);

            if (result.Succeeded == false) 
            {
                if (result.IsLockedOut) 
                {
                    AddError("User is temporarily blocked.");

                    return CreateResponse();
                }

                AddError("Email or password invalid.");

                return CreateResponse();
            }

            return CreateResponse(await CreateUserAuth(userLogin.Email));
        }

        private async Task<UserAuthenticationViewModel> CreateUserAuth(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var createdAtUtc = DateTime.UtcNow;
            var claims = await GetUserClaimsAndRolesAsync(user, createdAtUtc);

            var token = GetToken(claims, createdAtUtc, out var expirationUtc);

            return GetResponseToken(user, token, claims, createdAtUtc, expirationUtc);
        }

        private string GetToken(IList<Claim> claims, DateTime createdAtUtc, out DateTime expirationUtc)
        {
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var expires = createdAtUtc.AddHours(_appSettings.ExpirationHours);

            var securityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                Subject = identityClaims,
                Expires = expires,
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            expirationUtc = securityToken.ValidTo;

            return tokenHandler.WriteToken(securityToken);
        }

        private UserAuthenticationViewModel GetResponseToken(IdentityUser user,
                                                             string encodedToken,
                                                             IList<Claim> claims,
                                                             DateTime createdAtUtc,
                                                             DateTime expirationUtc)
        {
            return new UserAuthenticationViewModel
            {
                AccessToken = encodedToken,
                DateTimeAuth = new()
                {
                    CreatedAtUtc = createdAtUtc,
                    ExpirationInSeconds = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                    ExpirationUtc = expirationUtc
                },
                UserToken = new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim()
                    {
                        Type = c.Type,
                        Value = c.Value
                    })
                }
            };
        }

        private async Task<IList<Claim>> GetUserClaimsAndRolesAsync(IdentityUser user, DateTime createdAtUtc)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id)); // (Subject) Claim
            claims.Add(new(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); // (JWT ID) Claim
            claims.Add(new(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(createdAtUtc).ToString())); // (Not Before) Claim
            claims.Add(new(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(createdAtUtc).ToString(), ClaimValueTypes.Integer64)); //(Issued At) Claim

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
