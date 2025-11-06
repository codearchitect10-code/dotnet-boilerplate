using Framework.Core.Masters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Framework.Infrastructure.Persistence.Configuration.SysMasters;
public class EmiratesRegionConfiguration : IEntityTypeConfiguration<EmiratesRegion>
{
    public void Configure(EntityTypeBuilder<EmiratesRegion> builder)
    {
        builder.ToTable("EmiratesRegion", schema: "sysmaster");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .ValueGeneratedNever(); // prevents EF from marking it as Identity

        builder.Property(x => x.Value)
               .IsRequired()
               .HasMaxLength(200);

        builder.HasOne(x => x.Emirates)
               .WithMany(e => e.EmiratesRegions)
               .HasForeignKey(x => x.EmiratesId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
