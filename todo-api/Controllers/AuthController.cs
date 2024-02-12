using Microsoft.AspNetCore.Mvc;
using todo_api.IRepository;
using todo_api.IService;

namespace todo_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController: ControllerBase
    {
        private IAuthenticationService _IAuthentication;
        public AuthController(IAuthenticationService athentication) {
            _IAuthentication = athentication;
        }
        [HttpGet]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn(string UserName, string PassWord)
        {
            var res = await _IAuthentication.UserLogInAsync(UserName, PassWord);
            return Ok(Ok(new { UserInfo = res.Item1, Message = res.Item2 }));
        }
    }
}
