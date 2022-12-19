using ApiInventoryControl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiInventoryControl.Data.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);

            builder.Property(x => x.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("INT");

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("MONEY");

            builder
               .HasOne(x => x.Category)
               .WithMany(x => x.Products)
               .HasConstraintName("FK_Product_Category");

        }
    }
}
