using DomainDrivenDesignArchitucture.Infrastructure.Presistant.models.schemas.clients.roles;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.models.schemas.clients.users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.contexts;
internal class SqlIdentityContext : IdentityDbContext<User, Role, long>
{
    public SqlIdentityContext(DbContextOptions options) : base(options) { }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReferences.ASSEMBLY);
    }
}
