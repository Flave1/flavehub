using flavehub.Data;
using flavehub.Domain;
using flavehub.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks; 
using flavehub.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using flavehub.Contracts.ApiCore;

namespace flavehub.Repository.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly DataContext _dataContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IdentityService(UserManager<ApplicationUser> userManager, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters, DataContext dataContext, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _dataContext = dataContext;
            _roleManager = roleManager;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User Does Not Exist" }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User/Password Combination is wrong" }
                };
            }
            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string refreshToken, string token)
        {

            var validatedToken = GetClaimsPrincipalFromToken(token);
            if (validatedToken == null)
                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                 .AddDays(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
                return new AuthenticationResult { Errors = new[] { "This Token Hasn't Expired Yet" } };

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value;

            var storedRefreshToken = _dataContext.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
                return new AuthenticationResult { Errors = new[] { "This Refresh Token does not Exist" } };

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                return new AuthenticationResult { Errors = new[] { "This Refresh Token has Expired" } };

            if (storedRefreshToken.Invalidated)
                return new AuthenticationResult { Errors = new[] { "This Refresh Token has been Invalidated" } };

            if (storedRefreshToken.Used)
                return new AuthenticationResult { Errors = new[] { "This Refresh Token has been Used" } };

            if (storedRefreshToken.JwtId != jti)
                return new AuthenticationResult { Errors = new[] { "This Refresh Token Does not match this JWT" } };

            storedRefreshToken.Used = true;
            _dataContext.Update(storedRefreshToken);
            await _dataContext.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.SingleOrDefault(x => x.Type == "id").Value);

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        private ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                    return null;
                else
                    return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return validatedToken is JwtSecurityToken jwtSecurityToken &&
                            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                            StringComparison.InvariantCultureIgnoreCase);
        }
        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(email);

                if (existingUser != null)
                {
                    return new AuthenticationResult
                    {
                        Errors = new[] { "User with this email address already exist" }
                    };
                }

                var user = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                    UserLevel = UserLevel.Beginner, 
                };
                 
                var createdUser = await _userManager.CreateAsync(user, password); 

                //await AddUserToRoleAsync(user.Email);

                if (!createdUser.Succeeded)
                {
                    return new AuthenticationResult
                    {
                        Errors = createdUser.Errors.Select(x => x.Description)
                    };
                }

                return await GenerateAuthenticationResultForUserAsync(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            
            var claims = new List<Claim> 
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id), 
                new Claim("level", user.UserLevel.ToString()),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.UserName), 
            };
             
            var userClaims = await _userManager.GetClaimsAsync(user);

            claims.AddRange(userClaims);

            var userLevel = _userManager.Users.SingleOrDefault(m => m.Id == user.Id).UserLevel;

            if (userLevel.ToString() == null)claims.Add(new Claim("level", UserLevel.Beginner.ToString())); 

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach(var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole)??null);

                var role = await _roleManager.FindByNameAsync(userRole);

                if (role == null) 
                {
                    continue;
                }
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach(var roleClaim in roleClaims){
                    if (claims.Contains(roleClaim)) continue;
                    claims.Add(roleClaim);
                }
            }

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeSpan),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
             

            var token = tokenHandler.CreateToken(tokenDecriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddSeconds(6),
            };

            try
            {
                await _dataContext.RefreshTokens.AddAsync(refreshToken);
                await _dataContext.SaveChangesAsync();
            }catch(Exception ex)
            {
                return new AuthenticationResult {
                    Errors = new[] { $"Something went wrong: {ex.InnerException.Message}" }
                };
            }
            


            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token,
            };

            
        }

        public async Task<bool> AddUserToRoleAsync(string userEmail)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(userEmail);

                var userIsAdded = await _userManager.AddToRoleAsync(user, "Admin");

                if(user == null) 
                    return false; 

                if (userIsAdded.Succeeded) 
                    return true; 

                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
