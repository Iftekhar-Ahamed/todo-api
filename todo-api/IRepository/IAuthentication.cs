using todo_api.Helper;
using todo_api.Model.AuthModel;

namespace todo_api.IRepository
{
    public interface IAuthentication
    {
        Task<UserInfoModel?> UserLogInAsync(string UserName, string PassWord);
    }
}
