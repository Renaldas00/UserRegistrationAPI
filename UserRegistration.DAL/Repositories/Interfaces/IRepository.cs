using System.Linq.Expressions;

namespace UserRegistration.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);
        T? Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
