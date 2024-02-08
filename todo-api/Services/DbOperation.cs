using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using todo_api.Model.AuthModel;

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
    }
}
