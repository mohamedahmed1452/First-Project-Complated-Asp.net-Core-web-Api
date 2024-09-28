using Talabat.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Repository.Data.Config
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(b => b.Brand).WithMany().HasForeignKey(p => p.BrandId);
            builder.HasOne(t => t.Category).WithMany().HasForeignKey(p => p.CategoryId);
        }
    }
}
