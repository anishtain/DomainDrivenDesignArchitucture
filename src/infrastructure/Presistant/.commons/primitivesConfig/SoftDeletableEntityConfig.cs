using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitivesConfig;
internal class SoftDeletableEntityConfig<T, U> : ApprovableEntityConfig<T, U> where T : SoftDeletableEntity<U>
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.IsDeleted)
            .IsRequired();

        builder.HasIndex(x => x.IsDeleted)
            .IsClustered(false);
    }
}
