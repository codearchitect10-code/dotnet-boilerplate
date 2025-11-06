using Framework.Core.Masters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Framework.Infrastructure.Persistence.Configuration.SysMasters;
public class EmiratesConfiguration : IEntityTypeConfiguration<Emirates>
{
    public void Configure(EntityTypeBuilder<Emirates> builder)
    {
        builder.ToTable("Emirates", schema: "sysmaster");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .ValueGeneratedNever(); // prevents EF from marking it as Identity

        builder.Property(x => x.EmiratesCode)
               .IsRequired()
               .HasMaxLength(10);

        builder.Property(x => x.Value)
               .IsRequired()
               .HasMaxLength(200);
    }
}
