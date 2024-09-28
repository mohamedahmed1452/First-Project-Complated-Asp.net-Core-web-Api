using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        //one to many relationship
        public int BrandId { get; set; }//as foreign Key
        public ProductBrand Brand { get; set; } //as object


        //One to Many Relationship
        public int CategoryId { get; set; } //as foreign Key 
        public ProductCategory Category { get; set; } //as object
    }
}
