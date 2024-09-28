using Talabat.Core;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specification.Order_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository,
            IUnitOfWork unitOfWork,
            IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail,
            int deliveryMethodId, 
            string basketId, Address shippingAddress)
        {
            //1)  Get basket from the basket repo
            var basket=await _basketRepository.GetBasketAsync(basketId);
            //2)  Get items from the product repo
            var orderItems = new List<OrderItem>();
            if (basket?.Items.Count>0)
            {
                var productsRepository =  _unitOfWork.Repository<Product>();
                foreach (var item in basket.Items)
                {
                    var product= await productsRepository.GetAsync(item.Id);
                    var ProductItemOrder = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(ProductItemOrder, product.Price, item.Quantity);
                 orderItems.Add(orderItem);
                }
            }
            //3) Calculate subtotal

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            //4)Get DeliveryMethod from DeliveryMethodRepo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);
            var ordersRepo =  _unitOfWork.Repository<Order>();

            var orderSpecs= new OrderWithPaymentIntentSpecification(basket.PaymentIntentId);
            
            var existingOrder = await ordersRepo.GetEntityWithSpecAsync(orderSpecs);
            if(existingOrder is not null)
            {
                ordersRepo.Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }

            //4) Create order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal, basket.PaymentIntentId);
            await ordersRepo.AddAsync(order);
            //5) Save to db =. SaveChangesAsync

            var result=await _unitOfWork.CompleteAsync();
            if (result <= 0)
                return null;
            else return order;

        }
        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var ordersRepo = _unitOfWork.Repository<Order>();// 
            var spec = new OrderSpecification(buyerEmail);
            var orders = await ordersRepo.GetAllWithSpecAsync(spec);
            return orders;
        }
        public Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repository<Order>();//
            var orderSpec = new OrderSpecification(orderId, buyerEmail);
            var order = orderRepo.GetEntityWithSpecAsync(orderSpec);
            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
           var deliveryMethodRepo = _unitOfWork.Repository<DeliveryMethod>();
            return deliveryMethodRepo.GetAllAsync();
        }
    }
}
