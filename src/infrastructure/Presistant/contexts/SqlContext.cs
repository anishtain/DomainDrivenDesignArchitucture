namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.contexts;

internal class SqlContext : DbContext
{
    public SqlContext() : base() { }

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
