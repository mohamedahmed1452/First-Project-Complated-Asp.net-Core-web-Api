using Talabat.Core.Entities;
using Talabat.Core.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Repository
{
    internal class SpecificationEvaluator<TEntity>where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> query,ISpecification<TEntity> spec)
        {

            #region Filetation Using Where Condition 
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);//where p=>p.id==10
            }
            #endregion

            #region Sorting Using Order By
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);//order by p=>p.Name
            }
            else if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);//order by p=>p.Name
            }
            #endregion

            #region Enable Pagination
            if (spec.EnabledPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            #endregion

            #region Inner Join 
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));//inner join p=>p.ProductType and p=>p.ProductBrand
            #endregion

            return query;

        }
    }
}
