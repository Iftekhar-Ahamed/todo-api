using todo_api.Helper;
using todo_api.Model.TodoModel;
using todo_api.Model;

namespace todo_api.IService
{
    public interface ITodoService
    {
        Task<MessageHelperModel> CreateTaskAsync(CreateTaskModel createTaskModel);
        Task<PaginationLandingModel> GetAllTaskByUserIdAsync(long UserId, string? SearchTerm, TaskSortingModel taskSorting, long PageNo, long PageSize);
        Task<List<CommonDDL>> GetPriorityDDLAsync(string OrderBy);
        Task<MessageHelperModel> UpdateTaskByTaskIdAsync(UpdateTaskModel updateTaskModel);
        Task<MessageHelperModel> DeleteTaskByTaskIdAsync(long TaskId);
    }
}
