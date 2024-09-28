using Talabat.Core.Entities;

namespace Test_E_CommerceProject.Service.Dtos
{
    //map only not reverse map
    public class ProductToReturnDto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }


        public int BrandId { get; set; }//as foreign Key
        public string Brand { get; set; } //as object

        public int CategoryId { get; set; } //as foreign Key
        public string Category { get; set; } //as object

    }
}
