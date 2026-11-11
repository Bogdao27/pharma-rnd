using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaRnd.Domain;

namespace PharmaRnd.Infrastructure.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                id => id.Value,
                value => ProductId.Create(value));

            builder.ComplexProperty(p => p.ProductName, pb =>
            {
                pb.Property(pn => pn.Value)
                     .IsRequired()
                     .HasMaxLength(ProductName.MAX_PRODUCT_NAME_LENGTH)
                     .HasColumnName("product_name");
            });

            builder.ComplexProperty(p => p.Description, pb =>
            {
                pb.Property(d => d.Value)
                     .IsRequired()
                     .HasMaxLength(Description.MAX_DESCRIPTION_LENGTH)
                     .HasColumnName("description");
            });

            builder.ComplexProperty(p => p.DosageForm, pb =>
            {
                pb.Property(df => df.Value)
                     .IsRequired()
                     .HasMaxLength(DosageForm.MAX_DOSAGE_FORM_LENGTH)
                     .HasColumnName("dosage_form");
            });

            builder.HasMany(p => p.Batches)
                .WithOne()
                .HasForeignKey("product_id")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
