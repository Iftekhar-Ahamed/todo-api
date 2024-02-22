using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using todo_api.Helper;
using todo_api.IRepository;
using todo_api.Model;
using todo_api.Model.AuthModel;
using todo_api.Model.ReportModel;
using todo_api.Model.TodoModel;
using todo_api.Services;
namespace todo_api.Repository
{
    public class TodoRepository : ITodo
    {
        private readonly IConfiguration _configuration;
        public TodoRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<long> CreateTaskAsync(CreateTaskModel createTaskModel)
        {
            try
            {
                var sql = "INSERT INTO [dbo].[tblTask] " +
                          "([PriorityId],[TaskName],[TaskDescription],[UserId],[CreationDateTime],[ExpireDateTime],[TaskStatus],[isActive],[CreateBy],[Assigned])" +
                          "VALUES" +
                "(@PriorityId,@TaskName,@TaskDescription,@UserId,@CreationDateTime,@ExpireDateTime,@Status,1,@CreatorId,@AssignedId)";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var res = await connection.ExecuteAsync(sql, createTaskModel);
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<long> GetAllTaskCountByUserIdAsync(long UserId)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM dbo.tblTask AS t WHERE t.UserId = @UserId AND t.isActive = 1";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var res = await connection.QueryFirstOrDefaultAsync<long>(sql, new { UserId });
                    return res;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<TaskListWithCountModel> GetAllTaskByUserIdAsync(long UserId, string? SearchTerm,TaskSortingModel? taskSorting, long PageNo, long PageSize)
        {
            try
            {
                var FETCH = PageSize;
                var OFFSET = PageNo * PageSize;
                
                

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    using (var multiResult = await connection.QueryMultipleAsync("GetAllTask", new { UserId, OFFSET, FETCH, taskSorting?.Status, SearchTerm, taskSorting?.Priority, taskSorting?.creationDate }, commandType: CommandType.StoredProcedure))
                    {
                        var taskList = await multiResult.ReadAsync<GetAllTaskModel>();
                        var TotalRows = await multiResult.ReadSingleAsync<long>();

                        return new TaskListWithCountModel
                        {
                            TaskList = taskList.ToList(),
                            TaskCount = TotalRows
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<CommonDDL>> GetPriorityDDLAsync(string OrderBy)
        {
            try
            {
                var sql = "SELECT Priority AS value,PriorityName as name FROM dbo.tblPriority WHERE isActive = 1 ORDER BY Priority " + OrderBy;

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<CommonDDL>(sql, new { OrderBy });

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            
            }
        }
        public async Task<List<CommonDDL>> GetAllUserDDLAsync(string OrderBy)
        {
            try
            {
                var sql = "SELECT UserId AS value,FirstName+'['+CAST(UserId AS varchar)+']' as name FROM dbo.tblUser WHERE isActive = 1 ORDER BY UserId " + OrderBy;

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<CommonDDL>(sql, new { OrderBy });

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<long> UpdateTaskByTaskIdAsync(UpdateTaskModel updateTaskModel)
        {
            try
            {

                var sql = @"UPDATE [TODO].[dbo].[tblTask]
                             SET [TaskName] = @TaskName, 
                            [TaskDescription] = @TaskDescription,
                            [ExpireDateTime] = @ExpireDateTime,
                            [TaskStatus] = @Status,
                            [PriorityId] = @PriorityId,
                            [Assigned] = @AssignedId,
                            [CreationDateTime] = @CreationDateTime,
                            [EditedBy]= @UserId
                           WHERE [TaskId] = @TaskId";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var res = await connection.ExecuteAsync(sql, updateTaskModel );

                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ;
            }
        }
        public async Task<long> DeleteTaskByTaskIdAsync(long TaskId)
        {
            try
            {
                var sql = "UPDATE [TODO].[dbo].[tblTask]" +
                "SET [IsActive] = 0" +
                "WHERE [TaskId] = @TaskId";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var res = await connection.ExecuteAsync(sql, new { TaskId });

                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<TaskReportModel> UserTaskReportAsync(long UserId)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (var multiResult = await connection.QueryMultipleAsync("dbo.todoReport", new { UserId}, commandType: CommandType.StoredProcedure))
                    {
                        var taskReportModel = await multiResult.ReadFirstOrDefaultAsync<TaskReportModel>() ?? new TaskReportModel();
                        taskReportModel.TaskPriorityWiseReport = (await multiResult.ReadAsync<PriorityWiseTaskModel>()).ToList();
                        return taskReportModel;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
