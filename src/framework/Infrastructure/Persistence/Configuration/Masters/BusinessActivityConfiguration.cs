using Framework.Core.Masters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Framework.Infrastructure.Persistence.Configuration.Masters;
public class BusinessActivityConfiguration : IEntityTypeConfiguration<BusinessActivity>
{
    public void Configure(EntityTypeBuilder<BusinessActivity> builder)
    {
        builder.ToTable("BusinessActivity", schema: "master");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .ValueGeneratedNever(); // prevents EF from marking it as Identity

        builder.Property(x => x.ActivityCode)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.Value)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.IsActive)
               .HasDefaultValue(true);

        builder.HasOne(x => x.BusinessSubSector)
               .WithMany(s => s.BusinessActivities)
               .HasForeignKey(x => x.BusinessSubSectorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
