using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaRnd.Domain;

namespace PharmaRnd.Infrastructure.Configuration
{
    public class BatchComponentConfiguration : IEntityTypeConfiguration<BatchComponent>
    {
        public void Configure(EntityTypeBuilder<BatchComponent> builder)
        {
            builder.ToTable("batch_component");

            builder.HasKey(bc => bc.Id);

            builder.Property(bc => bc.Id)
                .HasConversion(
                     id => id.Value,
                     value => BatchComponentId.Create(value));

            builder.ComplexProperty(bc => bc.Mass, bcb =>
            {
                bcb.Property(m => m.Value)
                     .IsRequired()
                     .HasColumnName("mass");
            });

            builder.ComplexProperty(bc => bc.LotId, bcb =>
            {
                bcb.Property(l => l.Value)
                     .IsRequired()
                     .HasColumnName("lot_id");
            });

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted");
        }
    }
}
