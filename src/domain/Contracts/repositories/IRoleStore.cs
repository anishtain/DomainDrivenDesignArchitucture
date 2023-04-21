namespace DomainDrivenDesignArchitucture.Domain.Contracts.repositories;

public interface IRoleStore : IDisposable
{
    Task Create(string name, string persianName);

    Task<bool> IsRoleWithSpecificNameExists(string name);

    Task<string?> Get(string id);

    Task<long?> GetByName(string name);

    Task<IList<string>> GetCurrentPermissions();
}
