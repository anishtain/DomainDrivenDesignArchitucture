using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.contexts;
internal class SqlContext : DbContext
{
    public SqlContext(DbContextOptions options) : base(options) { }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReferences.ASSEMBLY);
    }
}
