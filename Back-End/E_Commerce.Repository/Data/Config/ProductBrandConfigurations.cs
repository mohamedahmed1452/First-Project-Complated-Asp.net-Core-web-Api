using Talabat.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Repository.Data.Config
{
    internal class ProductBrandConfigurations : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductBrand> builder)
        {

            //write propert suiable please
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);



        }
    }
}
