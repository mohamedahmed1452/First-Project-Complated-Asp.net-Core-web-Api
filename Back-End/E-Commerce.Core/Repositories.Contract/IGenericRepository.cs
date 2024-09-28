using Talabat.Core.Entities;
using Talabat.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        #region Work without specification
        Task<T?> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task AddAsync(T entity);//entity => order, product, deliveryMethod, etc
        void Update(T entity);
        void Delete(T entity);
        #endregion

        #region Work With Specification
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);

        Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<int> GetCount(ISpecification<T> spec);
        #endregion

    }
}
