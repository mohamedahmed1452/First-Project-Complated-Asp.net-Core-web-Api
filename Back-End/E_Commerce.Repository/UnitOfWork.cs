using Talabat.Core;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private Hashtable _repositories;


        public UnitOfWork(StoreContext dbContext)// ask clr to create object from DbContext implicitly
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }


        public async Task<int> CompleteAsync()
       => await _dbContext.SaveChangesAsync();

        public ValueTask DisposeAsync()
       => _dbContext.DisposeAsync();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name; //Order
            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(key, repository);
            }
            return (IGenericRepository<TEntity>)_repositories[key];
        }
    }
}
