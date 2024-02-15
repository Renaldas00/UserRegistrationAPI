using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    // This generic implementation of the IRepository interface
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _appDbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _appDbContext = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _appDbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _appDbContext.SaveChanges();
        }

        public virtual T? Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _appDbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _appDbContext.Entry(entity).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }
    }
}
