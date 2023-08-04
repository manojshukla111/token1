using JWT_Token_01.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Token_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("GetData")]
         public string GetData() {

            return "Authenticated with JWT";
        }
        [Authorize]
        [HttpPost]

     //   [Route("PostData")]

        public string addUser(Users user)
        {
            return "user added with username" + user.Username;
        }
    }
}
