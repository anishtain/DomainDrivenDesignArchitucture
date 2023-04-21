namespace DomainDrivenDesignArchitucture.Domain.Contracts.repositories;

public interface IUnitOfWork
{
    Task SaveChanges();
}
