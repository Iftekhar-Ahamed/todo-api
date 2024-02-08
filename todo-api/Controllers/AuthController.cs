using Microsoft.AspNetCore.Mvc;
using todo_api.IRepository;

namespace todo_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController: ControllerBase
    {
        private IAuthentication _IAuthentication;
        public AuthController(IAuthentication athentication) {
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
