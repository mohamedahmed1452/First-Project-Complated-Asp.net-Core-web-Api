using Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderAggregate
{
    public class DeliveryMethod:BaseEntity
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string description, decimal cost, string deliveryTime)
        {
            ShortName = shortName;
            Description = description;
            Cost = cost;
            DeliveryTime = deliveryTime;
        }

        public string ShortName { get; set; }// this is the name of the delivery method
        public string Description { get; set; }// this is the description of the delivery method
        public decimal Cost { get; set; }// this is the cost of the delivery method
        public string DeliveryTime { get; set; }// this is the delivery time of the delivery method

    }
}
