using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specification;
using Talabat.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext dbContext;
        public GenericRepository(StoreContext dbContext)//Ask Clr To Inject Object From StoreContext Implicitly
        {
            this.dbContext = dbContext;
        }
        #region Work Without Any Specification 
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IReadOnlyList<T>)await dbContext.Set<Product>().Include(p => p.Brand).Include(P => P.Category).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();//query to database
        }

       

        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                return await dbContext.Set<Product>().Where(p => p.Id == id).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync() as T;
            }
            return await dbContext.Set<T>().FindAsync(id);
        }


        public async Task AddAsync(T entity)
        => await dbContext.Set<T>().AddAsync(entity);

        public void  Update(T entity)
        =>  dbContext.Update(entity);

        public void Delete(T entity)
       => dbContext.Remove(entity);



        #endregion

        #region Work With Specification

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        { 
         return await ApplySpecification(spec).ToListAsync();

        }
        public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCount(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(dbContext.Set<T>(), spec);
        }



        #endregion

    }
}
