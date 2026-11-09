using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaRnd.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmaRnd.Infrastructure.Configuration
{
    public class BatchConfiguration : IEntityTypeConfiguration<Batch>
    {
        public void Configure(EntityTypeBuilder<Batch> builder)
        {
            builder.ToTable("batch");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                     id => id.Value,
                     value => BatchId.Create(value)
                );

            builder.ComplexProperty(b => b.ProdId, pb =>
            {
                pb.Property(p => p.Value)
                     .IsRequired()
                     .HasColumnName("product_id");
            });

            builder.ComplexProperty(b => b.Number, nb =>
            {
                nb.Property(n => n.Value)
                     .IsRequired()
                     .HasMaxLength(BatchNumber.BATCH_NUMBER_LENGTH)
                     .HasColumnName("batch_number");
            });

            builder.Property(b => b.ManufacturedOn);

            builder.HasMany(b => b.Components)
                .WithOne()
                .HasForeignKey("BatchId")
                .IsRequired();

            builder.Navigation(b => b.Components)
                .HasField("_components")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
