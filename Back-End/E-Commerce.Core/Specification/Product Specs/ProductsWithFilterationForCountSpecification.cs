using Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification.Product_Specs
{
    public class ProductsWithFilterationForCountSpecification:BaseSpecifications<Product>
    {
        public ProductsWithFilterationForCountSpecification(ProductSpecParams _params):base
            (
            P =>
            (string.IsNullOrEmpty(_params.Search) || P.Name.ToLower().Contains(_params.Search)) &&
            (!_params.BrandId.HasValue ||P.BrandId == _params.BrandId.Value) &&
            (!_params.CategoryId.HasValue || P.CategoryId == _params.CategoryId.Value))
        {
            
        }

    }
}
