using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaRnd.Domain;

namespace PharmaRnd.Infrastructure.Configuration
{
    public class BatchComponentConfiguration : IEntityTypeConfiguration<BatchComponent>
    {
        public void Configure(EntityTypeBuilder<BatchComponent> builder)
        {
            builder.ToTable("batch_components");

            builder.HasKey(bc => bc.Id);

            builder.Property(bc => bc.Id)
                .HasConversion(
                     id => id.Value,
                     value => BatchComponentId.Create(value)
                );

            builder.ComplexProperty(bc => bc.Mass, mb =>
            {
                mb.Property(m => m.Value)
                     .IsRequired()
                     .HasColumnName("mass_in_grams");
            });

            builder.ComplexProperty(bc => bc.LotId, lb =>
            {
                lb.Property(l => l.Value)
                     .IsRequired()
                     .HasColumnName("lot_id");
            });
        }
    }
}
