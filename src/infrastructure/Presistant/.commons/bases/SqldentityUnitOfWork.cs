using DomainDrivenDesignArchitucture.Domain.Contracts.repositories;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.contexts;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.bases;

internal class SqldentityUnitOfWork : IUnitOfWork
{
    private readonly SqlIdentityContext _context;

    internal SqldentityUnitOfWork(SqlIdentityContext context)
    {
        _context = context;
    }

    public async Task SaveChanges()
    {
        var entries = _context.ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Deleted when entry.Entity is SoftDeletableEntity<dynamic> entity:
                    entity.DeleterId = "1";
                    entity.DeletionDate = DateTime.UtcNow;
                    entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                    break;
                case EntityState.Modified when entry.Entity is AuditableEntity<dynamic> entity:
                    entity.UpdatorId = "1";
                    entity.UpdateDate = DateTime.UtcNow;
                    break;
                case EntityState.Added when entry.Entity is BaseEntity<dynamic> entity:
                    entity.CreatorId = "1";
                    entity.CreateDate = DateTime.UtcNow;
                    break;
                default:
                    break;
            }
        }

        await _context.SaveChangesAsync();
    }
}
