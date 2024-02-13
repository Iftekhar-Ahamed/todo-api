using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using todo_api.Helper;
using todo_api.IRepository;
using todo_api.IService;
using todo_api.Model.AuthModel;

namespace todo_api.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private IAuthentication db;
        IConfiguration _configuration;
        public AuthenticationService(IAuthentication todo, IConfiguration configuration) { 
            db = todo;
            _configuration = configuration;
        }

        public async Task<(UserInfoModel, MessageHelperModel)> UserLogInAsync(string UserName, string PassWord)
        {
            var user = await db.UserLogInAsync(UserName,PassWord);
           

            var msg = new MessageHelperModel { Message = "", StatusCode = 200 };

            if (user == null)
            {
                msg.Message = "Invalid UserName";
            }
            else
            {
                if (user.Password == PassWord)
                {
                    msg.Message = "Welcome User";
                    msg.Token = GenerateToken(user);
                }
                else
                {
                    msg.Message = "Invalid Password";
                }
                user.Password = null;
            }
            return (user, msg);
        }
        public string GenerateToken(UserInfoModel user)
        {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]??string.Empty),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(50),
                signingCredentials: signIn);

            var tk = new JwtSecurityTokenHandler().WriteToken(token);
            return tk;
        }
    }
}
