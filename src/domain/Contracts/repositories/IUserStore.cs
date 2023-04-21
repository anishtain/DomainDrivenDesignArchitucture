namespace DomainDrivenDesignArchitucture.Domain.Contracts.repositories;

public interface IUserStore
{
    Task<string> PasswordLogin(string username, string password);

    Task RegisterUser(string username, string password);
}
