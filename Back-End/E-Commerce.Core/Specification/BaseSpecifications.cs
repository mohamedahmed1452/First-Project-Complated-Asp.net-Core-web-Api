using Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; } = null;
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;
        public int Take { get ; set ; }
        public int Skip { get ; set ; }
        public bool EnabledPagination { get; set; }=false;


        public BaseSpecifications()//include Only without using where
        {
        }
        public BaseSpecifications(Expression<Func<T, bool>> CriteriaExpresion)//where 
        {
            Criteria= CriteriaExpresion; //p=>p.id==10
        }

        //just Setter only
        public void AddOrderBy(Expression<Func<T,object>> orderBy)
        {
            this.OrderBy = orderBy;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
        {
            this.OrderByDesc = orderByDesc;
        }

        public void ApplyPagination(int skip, int take)
        {
            EnabledPagination = true;
            Skip = skip;
            Take = take;

        }
    }
}
