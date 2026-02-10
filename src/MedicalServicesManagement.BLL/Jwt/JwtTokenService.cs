using MedicalServicesManagement.DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

namespace MedicalServicesManagement.BLL.Jwt
{
    public class JwtTokenService
    {
        private const int DaysExpirationTerm = 50; //срок действия
        private const string UserIdClaim = "userId";
        private const string RoleNameClaim = "roleName";

        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly string _jwtSecretKey;

        public JwtTokenService(IOptions<JwtTokenSettings> options)
        {
            if (options != null)
            {
                var optionsValue = options.Value;
                _jwtIssuer = optionsValue.JwtIssuer;
                _jwtAudience = optionsValue.JwtAudience;
                _jwtSecretKey = optionsValue.JwtSecretKey;
            }
        }

        public string GetToken(AuthUser user, IList<string> roles, string roleName)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Добавление клеймов (набор утверждений) в список в JWT токене.
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));
            var userClaims = new List<Claim>
            {
                new Claim(type: UserIdClaim, user.Id),
                new Claim(type: JwtRegisteredClaimNames.GivenName, user.FullName),
                new Claim(type: RoleNameClaim, roleName),
            };

            userClaims.AddRange(roleClaims);

            var jwt = new JwtSecurityToken(
                issuer: _jwtIssuer,//издатель
                audience: _jwtAudience,//аудитория
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey)),
                    SecurityAlgorithms.HmacSha256),
                claims: userClaims,
                expires: DateTime.Now.AddDays(DaysExpirationTerm));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _jwtIssuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtAudience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey)),
                    ValidateLifetime = true,
                    LifetimeValidator =
                        (before, exprise, localToken, parameters) =>
                        {
                            if (exprise.HasValue)
                            {
                                return exprise.Value > DateTime.UtcNow;
                            }

                            return true;
                        },
                    RequireExpirationTime = true,
                },
                out var _);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }
    }
}
