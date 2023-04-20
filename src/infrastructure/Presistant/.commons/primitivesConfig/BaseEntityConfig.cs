using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitivesConfig;
internal class BaseEntityConfig<T, U> : IEntityTypeConfiguration<T> where T : BaseEntity<U>
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatorId)
            .IsRequired()
            .HasColumnType("varchar(64)");

        builder.Property(x => x.CreateDate)
            .IsRequired();

        builder.HasIndex(x => x.CreatorId)
            .IsClustered(false);
    }
}
