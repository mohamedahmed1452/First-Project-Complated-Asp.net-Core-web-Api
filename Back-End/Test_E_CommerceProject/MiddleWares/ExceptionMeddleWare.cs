using System.Net;
using System.Text.Json;
using Test_E_CommerceProject.Service.Errors;

namespace Test_E_CommerceProject.Service.MiddleWares
{
    public class ExceptionMeddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMeddleWare> logger;
        private readonly IHostEnvironment env;

        //by conventions

        public ExceptionMeddleWare(RequestDelegate next,ILogger<ExceptionMeddleWare> logger,IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

       public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                //log Exception in database or files 
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode =(int) HttpStatusCode.InternalServerError;
                var response = env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()): new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
               var options=new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json=JsonSerializer.Serialize(response, options);

                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
