using Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification
{
    public interface ISpecification<T> where T : BaseEntity
    {
        //Create property Signature For Each and Every Specification
        public Expression<Func<T,bool>> Criteria { get; set; } // Criteria where
        //p=>p.id==i
        public List<Expression<Func<T,object>>> Includes { get; set; } //inner join 


        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool EnabledPagination { get; set; }
        public void ApplyPagination(int skip ,int take); 

    }
}
