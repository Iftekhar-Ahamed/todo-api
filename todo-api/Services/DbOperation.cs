using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using todo_api.Helper;
using todo_api.Model.AuthModel;
using todo_api.Model.TodoModel;

namespace todo_api.Services
{
    public class DbOperation
    {
        private readonly IConfiguration _configuration;
        public DbOperation(IConfiguration configuration) {
            this._configuration = configuration;
        }
        public async Task<UserInfoModel?> GetUserDetailsAsync(string UserName)
        {
            try
            {
                var sql = "SELECT * FROM dbo.tblUser WHERE userName = @UserName";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<UserInfoModel>(sql, new { UserName });
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public async Task<long> CreateTaskAsync(CreateTaskModel createTaskModel)
        {
            try
            {
                var sql = "INSERT INTO [dbo].[tblTask] " +
                          "([PriorityId],[TaskName],[TaskDescription],[UserId],[CreationDateTine],[ExpireDateTime],[isActive])" +
                          "VALUES" +
                          "(@PriorityId,@TaskName,@TaskDescription,@UserId,@ExpireDateTime,@CreationDateTine,1)";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, createTaskModel);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<GetAllTaskModel>> GetAllTaskByUserIdAsync(long UserId,string OrderBy, long? OFFSET, long? FETCH)
        {
            try
            {
                var sql = "SELECT * FROM dbo.tblTask WHERE UserId = @UserId AND isActive = 1 ORDER BY CreationDateTime "+OrderBy;
                if (OFFSET != null && FETCH != null)
                {
                    sql = sql + " OFFSET " + OFFSET + " ROWS FETCH NEXT " + FETCH + " ROWS ONLY";
                }
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<GetAllTaskModel>(sql,new { UserId });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
