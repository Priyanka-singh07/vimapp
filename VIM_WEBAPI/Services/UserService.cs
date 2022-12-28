namespace VIM_WEBAPI.Services
{
    public class UserService : IUserService
    {
        public bool ValidateCredentials(string username, string password)
        {
            return username.Equals("vimuser") && password.Equals("Vim@2022");
        }
    }
}
