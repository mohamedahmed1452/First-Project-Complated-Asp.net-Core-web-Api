using Talabat.Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Repository.Data.Config
{
    class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, ShappingAddress => ShappingAddress.WithOwner());

            builder.Property(o=>o.Status).HasConversion(OStatus=>OStatus.ToString(),
                OStatus=>(OrderStatus)Enum.Parse(typeof(OrderStatus),OStatus));

            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
        }
    }
}
