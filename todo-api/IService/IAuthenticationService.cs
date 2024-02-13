using todo_api.Helper;
using todo_api.Model.AuthModel;

namespace todo_api.IService
{
    public interface IAuthenticationService
    {
        Task<(UserInfoModel, MessageHelperModel)> UserLogInAsync(string UserName, string PassWord);
        string GenerateToken(UserInfoModel user);
    }
}
