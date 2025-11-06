using Framework.Core.Masters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Framework.Infrastructure.Persistence.Configuration.Masters;
public class BusinessSectorConfiguration : IEntityTypeConfiguration<BusinessSector>
{
    public void Configure(EntityTypeBuilder<BusinessSector> builder)
    {
        builder.ToTable("BusinessSector", schema: "master");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .ValueGeneratedNever(); // prevents EF from marking it as Identity

        builder.Property(x => x.Value)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.IsActive)
               .HasDefaultValue(true);
    }
}
