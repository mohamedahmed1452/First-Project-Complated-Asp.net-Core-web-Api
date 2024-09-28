using System.ComponentModel.DataAnnotations;

namespace Test_E_CommerceProject.Service.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1, Double.MaxValue,ErrorMessage ="Price Must Be Greater Than Zero")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Quantity Must Be At Least one Item")]
        public int Quantity { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Category { get; set; }
    }
}