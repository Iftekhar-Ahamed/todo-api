namespace todo_api.Model.AuthModel
{
    public class UserInfoModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long UserType {  get; set; }
        public string? Password { get; set; }
    }
}
