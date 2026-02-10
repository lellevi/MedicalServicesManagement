using MedicalServicesManagement.BLL.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace MedicalServicesManagement.WebApp.Jwt
{
    public class JwtAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly JwtTokenService _jwtTokenService;

        public JwtAuthenticationHandler(
            JwtTokenService jwtTokenService,
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder)
            : base(options, logger, encoder)
        {
            _jwtTokenService = jwtTokenService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Cookies.ContainsKey(Constants.JwtCookiesKey))//проверка токена в куки
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var token = Request.Cookies[Constants.JwtCookiesKey]!.ToString();

            var isValid = _jwtTokenService.ValidateToken(token);

            if (isValid)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                //содержит набор утверждений пользователя,название схемы аутентификации
                var identity = new ClaimsIdentity(jwtToken.Claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Unauthorized");
        }
    }
}
