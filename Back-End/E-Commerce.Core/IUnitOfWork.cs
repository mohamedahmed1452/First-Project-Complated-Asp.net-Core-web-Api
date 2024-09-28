using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core
{
    public interface IUnitOfWork : IAsyncDisposable 
    {

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity:BaseEntity;
        Task<int> CompleteAsync();





    }



}
