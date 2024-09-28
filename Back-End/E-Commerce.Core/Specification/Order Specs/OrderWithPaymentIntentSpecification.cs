using Talabat.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification.Order_Specs
{
    public class OrderWithPaymentIntentSpecification: BaseSpecifications<Order>
    {
        public OrderWithPaymentIntentSpecification(string paymentIntentId) : base(o => o.PaymentIntenteId == paymentIntentId)
        {

        }
    }
}
