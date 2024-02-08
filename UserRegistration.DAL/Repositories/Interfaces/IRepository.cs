using System.Linq.Expressions;

namespace UserRegistration.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets all entities of type T from the repository, with optional related entities included.
        /// </summary>
        /// <param name="includeProperties">Optional navigation properties to include in the query results.</param>
        /// <returns>An IQueryable collection of entities of type T.</returns>
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Gets the entity of type T with the specified ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The entity of type T with the specified ID, or null if not found.</returns>
        T? Get(int id);

        /// <summary>
        /// Adds a new entity of type T to the repository.
        /// </summary>
        /// <param name="entity">The entity of type T to add.</param>
        void Add(T entity);

        /// <summary>
        /// Updates an existing entity of type T in the repository.
        /// </summary>
        /// <param name="entity">The entity of type T to update.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity of type T from the repository.
        /// </summary>
        /// <param name="entity">The entity of type T to delete.</param>
        void Delete(T entity);
    }
}
