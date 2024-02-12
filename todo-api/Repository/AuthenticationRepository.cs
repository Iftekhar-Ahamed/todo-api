using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using todo_api.Helper;
using todo_api.IRepository;
using todo_api.Model.AuthModel;
using todo_api.Services;

namespace todo_api.Repository
{
    public class AuthenticationRepository : IAuthentication
    {
        private readonly IConfiguration _configuration;
        public AuthenticationRepository(IConfiguration configuration) {
            this._configuration = configuration;
        }
        
        public async Task<UserInfoModel?> UserLogInAsync(string UserName, string PassWord)
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
