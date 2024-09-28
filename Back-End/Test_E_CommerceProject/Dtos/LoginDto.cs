using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace Test_E_CommerceProject.Service.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
    }
}
