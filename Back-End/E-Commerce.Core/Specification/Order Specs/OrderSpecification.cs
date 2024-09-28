using Talabat.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification.Order_Specs
{
    public class OrderSpecification : BaseSpecifications<Order>
    {
       
        public OrderSpecification(string buyerEmail):base(o=>o.BuyerEmail==buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
            AddOrderByDesc(o => o.OrderDate);
        }
        public OrderSpecification(int orderId,string buyerEmail) 
            :base(O=>O.Id==orderId && O.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
            AddOrderByDesc(o => o.OrderDate);
        }
      

    }
}
