using Talabat.Core;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Microsoft.AspNetCore.Mvc;
using Talabat.Service;
using Test_E_CommerceProject.Service.Errors;
using Test_E_CommerceProject.Service.Helpers;

namespace Test_E_CommerceProject.Service.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            Services.AddScoped(typeof(IProductService), typeof(ProductService));
            Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            Services.AddScoped<IBasketRepository,Basketrepository>();
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MappingProfiles));
            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                    .SelectMany(p => p.Value.Errors)
                    .Select(E => E.ErrorMessage).ToList();

                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });

          return Services;
        }
    }
}
