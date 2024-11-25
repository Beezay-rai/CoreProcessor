using IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        [HttpGet,Route("/token")]
        public IActionResult Token(LoginModel model)
        {
            return Ok(model);
        }
    }
}
