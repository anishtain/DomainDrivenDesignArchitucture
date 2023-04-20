using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitivesConfig;
internal class ApprovableEntityConfig<T, U> : AuditableEntityConfig<T, U> where T : ApprovableEntity<U>
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.IsApproved)
            .IsRequired();

        builder.HasIndex(x => x.IsApproved)
            .IsClustered(false);
    }
}
