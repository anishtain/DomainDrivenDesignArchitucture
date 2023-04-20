using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitivesConfig;
internal class AuditableEntityConfig<T, U> : BaseEntityConfig<T, U> where T : AuditableEntity<U>
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.UpdatorId)
            .HasColumnType("varchar(64)");

        builder.HasIndex(x => x.UpdatorId)
            .IsClustered(false);
    }
}
