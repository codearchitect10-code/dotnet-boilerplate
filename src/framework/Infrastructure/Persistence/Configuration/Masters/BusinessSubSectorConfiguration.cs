using Framework.Core.Masters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Framework.Infrastructure.Persistence.Configuration.Masters;
public class BusinessSubSectorConfiguration : IEntityTypeConfiguration<BusinessSubSector>
{
    public void Configure(EntityTypeBuilder<BusinessSubSector> builder)
    {
        builder.ToTable("BusinessSubSector", schema: "master");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .ValueGeneratedNever(); // prevents EF from marking it as Identity

        builder.Property(x => x.Value)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.IsActive)
               .HasDefaultValue(true);

        builder.HasOne(x => x.BusinessSector)
               .WithMany(s => s.BusinessSubSectors)
               .HasForeignKey(x => x.BusinessSectorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
