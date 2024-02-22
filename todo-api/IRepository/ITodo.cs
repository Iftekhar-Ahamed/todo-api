using todo_api.Helper;
using todo_api.Model;
using todo_api.Model.ReportModel;
using todo_api.Model.TodoModel;

namespace todo_api.IRepository
{
    public interface ITodo
    {
        Task<long> CreateTaskAsync(CreateTaskModel createTaskModel);
        Task<long> GetAllTaskCountByUserIdAsync(long UserId);
        Task<TaskListWithCountModel> GetAllTaskByUserIdAsync(long UserId, string? SearchTerm, TaskSortingModel? taskSorting, long PageNo, long PageSize);
        Task<List<CommonDDL>> GetPriorityDDLAsync(string OrderBy);
        Task<List<CommonDDL>> GetAllUserDDLAsync(string OrderBy);
        Task<long> UpdateTaskByTaskIdAsync(UpdateTaskModel updateTaskModel);
        Task<long> DeleteTaskByTaskIdAsync(long TaskId);
        Task<TaskReportModel> UserTaskReportAsync(long UserId);
    }
}
