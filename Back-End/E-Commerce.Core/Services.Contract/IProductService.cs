using Talabat.Core.Entities;
using Talabat.Core.Specification.Product_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services.Contract
{
    public interface IProductService
    {
         Task<IReadOnlyList<Product>> GetProductAsync(ProductSpecParams? _params);
         Task<Product?> GetProductAsync(int id);

        Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
        Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync();

        Task<int> GetCountAsync(ProductSpecParams _params);

    }
}
