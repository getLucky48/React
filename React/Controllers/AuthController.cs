using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace React.Controllers
{
    [Route("api/auth")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost]
        public IActionResult Login([FromBody] object json)
        {

            Dictionary<string, string> requestData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json.ToString());

            string login = requestData["username"];
            string password = requestData["password"];

            if (login.Equals("admin") && password.Equals("admin")) { return Ok(); }

            return Unauthorized();

        }

    }

}
