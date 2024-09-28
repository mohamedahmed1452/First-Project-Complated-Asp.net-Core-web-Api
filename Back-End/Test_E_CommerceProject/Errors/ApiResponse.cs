using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Test_E_CommerceProject.Service.Errors
{
    public class ApiResponse
    {

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int _statusCode,string? _message=null)
        {
            StatusCode= _statusCode;
            Message = _message ?? GetDefaultMessageForStatusCode(StatusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "UnAuthorized",
                404=>"Resource Was Not Found",
                500=> "Error Are The Path To THe Dark Side, Error Lead To Anger, Anger Lead To Hate, Hate Lead To Career Change",
                _ => null
            };
        }
    }
}
