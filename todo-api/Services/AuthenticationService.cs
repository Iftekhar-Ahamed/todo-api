using todo_api.Helper;
using todo_api.IRepository;
using todo_api.IService;
using todo_api.Model.AuthModel;

namespace todo_api.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private IAuthentication db;
        public AuthenticationService(IAuthentication todo) { 
            db = todo;
        }

        public async Task<(UserInfoModel, MessageHelperModel)> UserLogInAsync(string UserName, string PassWord)
        {
            var user = await db.UserLogInAsync(UserName,PassWord);
           

            var msg = new MessageHelperModel { Message = "", StatusCode = 200 };

            if (user == null)
            {
                msg.Message = "Invalid UserName";
            }
            else
            {
                if (user.Password == PassWord)
                {
                    msg.Message = "Welcome User";
                }
                else
                {
                    msg.Message = "Invalid Password";
                }
                user.Password = null;
            }
            return (user, msg);
        }
    }
}
