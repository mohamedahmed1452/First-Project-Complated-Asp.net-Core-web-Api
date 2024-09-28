using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.ProductSpec;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specification.Product_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       
        public Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
       => _unitOfWork.Repository<ProductBrand>().GetAllAsync();

        public Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
        => _unitOfWork.Repository<ProductCategory>().GetAllAsync();

        public async Task<int> GetCountAsync(ProductSpecParams _params)
        {
            var specCount = new ProductsWithFilterationForCountSpecification(_params);
            int Count = await _unitOfWork.Repository<Product>().GetCount(specCount);
            return Count;
        }

        public async Task<IReadOnlyList<Product>> GetProductAsync(ProductSpecParams? _params)
        {
            var spec = new ProductWithBrandAndCategorySpecification(_params);
            var products =  await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            return products;
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecification(id);
            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(spec);
            return product;
        }
    }
}
