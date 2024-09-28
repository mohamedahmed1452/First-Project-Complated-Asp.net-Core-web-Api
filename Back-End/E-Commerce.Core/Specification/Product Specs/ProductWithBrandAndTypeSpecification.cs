using Talabat.Core.Entities;
using Talabat.Core.Specification;
using Talabat.Core.Specification.Product_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Talabat.Core.ProductSpec
{
    public class ProductWithBrandAndCategorySpecification:BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecification(ProductSpecParams _params):
        #region Filteration
                    base(P =>
            (string.IsNullOrEmpty(_params.Search)||P.Name.ToLower().Contains(_params.Search))&&
            (!_params.BrandId.HasValue || P.BrandId == _params.BrandId.Value) &&
            (!_params.CategoryId.HasValue || P.CategoryId == _params.CategoryId.Value)) 
        #endregion
        {
            #region Inner Join
            AddIncludes();
            #endregion
            #region Sorting [Ascending Or Descending using [Price and Name]]
            if (!string.IsNullOrEmpty(_params.Sort))
            {
                switch (_params.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
            else
                AddOrderBy(p => p.Name);
            #endregion

            #region Apply Pagination
            ApplyPagination((_params.PageIndex - 1) * _params.PageSize, _params.PageSize);
            #endregion

        }

      

        public ProductWithBrandAndCategorySpecification(int id) : base(x => x.Id == id)
        {
            AddIncludes();
        }
        private void AddIncludes()
        {
            Includes.Add(x => x.Brand);
            Includes.Add(x => x.Category);
        }
    }
}
