using eShopper.Core.Entities;
using eShopper.Core.Interfaces;
using System.Collections;

namespace eShopper.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private Hashtable _repositories;
        public UnitOfWork(StoreContext context)
        {
            _storeContext =context;
        }
        public async Task<int> Complete()
        {
            return await _storeContext.SaveChangesAsync();  
        }

        public void Dispose()
        {
           _storeContext.Dispose(); 
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
           if(_repositories == null) { _repositories = new Hashtable(); }   

           var type = typeof(TEntity).Name;

            if(!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.
                    MakeGenericType(typeof(TEntity)), _storeContext);

                _repositories.Add(type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>) _repositories[type];
        }
    }
}
