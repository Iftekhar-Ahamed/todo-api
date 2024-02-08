using todo_api.Helper;
using todo_api.IRepository;
using todo_api.Model.AuthModel;
using todo_api.Services;

namespace todo_api.Repository
{
    public class AuthenticationRepository : IAuthentication
    {
        private DbOperation db;
        public AuthenticationRepository(DbOperation dbOperation ) { 
            db = dbOperation;
        }
        public async Task<(UserInfoModel, MessageHelperModel)> UserLogInAsync(string UserName, string PassWord)
        {
            var user = await db.GetUserDetailsAsync(UserName);
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
