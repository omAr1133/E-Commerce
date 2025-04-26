﻿


using Domain.Models.Products;

namespace Persistence.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand)
                 .WithMany()
                 .HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.ProductType)
                 .WithMany()
                 .HasForeignKey(p => p.TypeId);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(10,3)");
        }
    }
}
