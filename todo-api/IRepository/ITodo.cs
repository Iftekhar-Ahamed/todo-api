using todo_api.Helper;
using todo_api.Model;
using todo_api.Model.TodoModel;

namespace todo_api.IRepository
{
    public interface ITodo
    {
        Task<long> CreateTaskAsync(CreateTaskModel createTaskModel);
        Task<List<GetAllTaskModel>> GetAllTaskByUserIdAsync(long UserId, string OrderBy, long PageNo, long PageSize);
        Task<List<CommonDDL>> GetPriorityDDLAsync(string OrderBy);
        Task<long> UpdateTaskByTaskIdAsync(UpdateTaskModel updateTaskModel);
        Task<long> DeleteTaskByTaskIdAsync(long TaskId);
    }
}
