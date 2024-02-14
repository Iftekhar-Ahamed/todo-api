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
            var claims = new List<Claim>{new Claim(ClaimTypes.Name, user.FirstName)};

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
