using Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services.Contract
{
    public interface IPaymentService
    {

        public Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);


    }
}
