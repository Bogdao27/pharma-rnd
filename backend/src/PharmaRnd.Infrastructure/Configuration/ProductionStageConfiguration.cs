using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaRnd.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmaRnd.Infrastructure.Configuration
{
    public class ProductionStageConfiguration : IEntityTypeConfiguration<ProductionStage>
    {
        public void Configure(EntityTypeBuilder<ProductionStage> builder)
        {
            builder.ToTable("production_stages");

            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.Id)
                .HasConversion(
                id => id.Value,
                value => ProductionStageId.Create(value));

            builder.Property(ps => ps.ExecutedAt)
                .IsRequired()
                .HasColumnName("executed_at");

            builder.ComplexProperty(ps => ps.StageDescription, s =>
            {
                s.Property(d => d.Value)
                     .IsRequired()
                     .HasColumnName("stage_description")
                     .HasMaxLength(StageDescription.MAX_LENGTH);
            });

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted");
        }
    }
}
