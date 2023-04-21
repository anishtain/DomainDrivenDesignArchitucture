namespace DomainDrivenDesignArchitucture.Domain.Contracts.repositories;

public interface IRoleStore
{
    Task Create(string name, string persianName);

    Task<string?> Get(string id);

    Task<long?> GetByName(string name);
}
