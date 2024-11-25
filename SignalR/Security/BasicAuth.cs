using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace SignalR.Security
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                    return await Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));

                var authHeader = Request.Headers["Authorization"].ToString();
                var authenticationValue = authHeader.Substring("Basic ".Length).Trim();
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationValue));
                var parts = credentials.Split(':');
                var username = parts[0];
                var password = parts[1];

                if (ValidateCredentials(username, password))
                {
                    var claims = new[] {
                new Claim(ClaimTypes.Name, username),
            };

                    var identity = new ClaimsIdentity(claims, "Basic");
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, "Basic");

                    return await Task.FromResult(AuthenticateResult.Success(ticket));
                }
                else
                {
                    return await Task.FromResult(AuthenticateResult.Fail("Invalid credentials"));
                }
            }
            catch
            {
                return await Task.FromResult(AuthenticateResult.Fail("Invalid credentials"));

            }


        }

        private bool ValidateCredentials(string username, string password)
        {
            return username == "Bijay" && password == "password";
        }
    }
}
