using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
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
                          "([PriorityId],[TaskName],[TaskDescription],[UserId],[CreationDateTime],[ExpireDateTime],[Status],[isActive])" +
                          "VALUES" +
                "(@PriorityId,@TaskName,@TaskDescription,@UserId,@CreationDateTime,@ExpireDateTime,@Status,1)";

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
                    var res = await connection.QueryFirstOrDefaultAsync<long>(sql,new { UserId });
                    return res;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<GetAllTaskModel>> GetAllTaskByUserIdAsync(long UserId, string? SearchTerm,TaskSortingModel? taskSorting, long PageNo, long PageSize)
        {
            try
            {
                var FETCH = PageSize;
                var OFFSET = PageNo * PageSize;
                var sql = "SELECT " +
                         "t.TaskId as TaskId, t.PriorityId as PriorityId, p.PriorityName as PriorityName, " +
                         "t.TaskName as TaskName, t.TaskDescription as TaskDescription, t.UserId as UserId, " +
                         "t.ExpireDateTime as ExpireDateTime, t.CreationDateTime as CreationDateTime, t.Status as Status " +
                         "FROM dbo.tblTask as t " +
                         "INNER JOIN dbo.tblPriority as p on t.PriorityId = p.Priority " +
                         "WHERE t.UserId = @UserId AND t.isActive = 1  ";
                if (SearchTerm != null)
                {
                    sql += "AND t.TaskName Like '%"+SearchTerm+"%'";
                }
                    if (taskSorting != null && (taskSorting.Priority != null || taskSorting.creationDate != null || taskSorting.Status!= null))
                {
                    sql += "ORDER BY ";
                    bool add = false;
                    if (taskSorting.Status != null)
                    {
                        if (add) sql += " ,";
                        sql += " t.Status " + taskSorting.Status;
                        add = true;
                    }
                    if (taskSorting.Priority != null)
                    {
                        if (add) sql += " ,";
                        sql += " t.PriorityId " + taskSorting.Priority;
                        add = true;
                    }
                    if (taskSorting.creationDate != null)
                    {
                        if (add) sql += " ,";
                        sql += "t.CreationDateTime "+taskSorting.creationDate;
                        add = true;
                    }   
                }

                
                sql += " OFFSET @OFFSET ROWS FETCH NEXT @FETCH ROWS ONLY";
                

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var res = await connection.QueryAsync<GetAllTaskModel>(sql, new { UserId, OFFSET, FETCH,taskSorting?.Priority,taskSorting?.creationDate, taskSorting?.Status,SearchTerm });
                    return res.ToList();
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
        public async Task<long> UpdateTaskByTaskIdAsync(UpdateTaskModel updateTaskModel)
        {
            try
            {
                var sql = "UPDATE [TODO].[dbo].[tblTask]" +
                            "SET [TaskName] = @TaskName," +
                            "[TaskDescription] = @TaskDescription," +
                            "[ExpireDateTime] = @ExpireDateTime," +
                            "[Status] = @Status," +
                            "[PriorityId] = @PriorityId," +
                            "[CreationDateTime] = @CreationDateTime " +
                "WHERE [TaskId] = @TaskId";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var res = await connection.ExecuteAsync(sql, updateTaskModel);

                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
