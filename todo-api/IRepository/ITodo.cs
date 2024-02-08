using todo_api.Helper;
using todo_api.Model;
using todo_api.Model.TodoModel;

namespace todo_api.IRepository
{
    public interface ITodo
    {
        Task<MessageHelperModel> CreateTaskAsync(CreateTaskModel createTaskModel);
        Task<PaginationLandingModel> GetAllTaskByUserIdAsync(long UserId, string OrderBy, long PageNo, long PageSize);
    }
}
