using DomainDrivenDesignArchitucture.Infrastructure.Presistant.models.schemas.clients.roles;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.models.schemas.clients.users;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.contexts;
internal class SqlIdentityContext : IdentityDbContext<User, Role, long>
{
    public SqlIdentityContext() : base() { }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer("Integrated Security = SSPI; Persist Security Info=False; Initial Catalog = DomainDrivenDesignDb; Data Source =.");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReferences.ASSEMBLY);
    }
}
