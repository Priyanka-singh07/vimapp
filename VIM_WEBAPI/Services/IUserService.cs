namespace VIM_WEBAPI.Services
{
    public interface IUserService
    {
        bool ValidateCredentials(string username, string password);
    }
}
