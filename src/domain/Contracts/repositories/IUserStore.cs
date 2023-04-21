namespace DomainDrivenDesignArchitucture.Domain.Contracts.repositories;

public interface IUserStore : IDisposable
{
    Task<string> PasswordLogin(string username, string password);

    Task RegisterUser(string username, string password, string roleName);

    Task<bool> IsUserWithSpecificRoleExists(string role);

    Task<string> GetCurrentUserId();
}
