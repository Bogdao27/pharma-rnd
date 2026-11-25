using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaRnd.Domain;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Infrastructure.Configuration
{
    public class BatchConfiguration : IEntityTypeConfiguration<Batch>
    {
        public void Configure(EntityTypeBuilder<Batch> builder)
        {
            builder.ToTable("batches");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                     id => id.Value,
                     value => BatchId.Create(value));

            builder.ComplexProperty(b => b.Number, bb =>
            {
                bb.Property(bn => bn.Value)
                     .IsRequired()
                     .HasMaxLength(BatchNumber.BATCH_NUMBER_LENGTH)
                     .HasColumnName("batch_number");
            });

            builder.Property(b => b.ManufacturedOn)
                .IsRequired();

            builder.HasMany(b => b.Components)
                .WithOne()
                .HasForeignKey("batch_id")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.Stages)
                .WithOne()
                .HasForeignKey("batch_id")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(b => b.Files, fb =>
            {
                fb.ToJson("files");

                fb.OwnsMany(fi => fi.Values, fileBuilder =>
                {
                    fileBuilder.Property(fa => fa.PathToStorage)
                        .HasConversion(
                            p => p.Path, 
                            value => FilePath.Create(value).Value)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                });
            });

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted");
        }
    }
}
