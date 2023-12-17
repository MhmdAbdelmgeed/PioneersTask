using System.Linq.Expressions;

namespace GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync(
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<string>? includes = null
            );


        Task<T> GetAsync(Expression<Func<T, bool>> expression,
           List<string>? includes = null);

        Task InsertAsync(T entity);

        Task InsertRangeAsync(IEnumerable<T> entities);

        Task DeleteAsync(Guid id);

        void DeleteRange(IEnumerable<T> entities);

        void Update(T entity);
        IList<T> GetAll(Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<string>? includes = null);

        IQueryable<T> GetAllIQueryable(Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<string>? includes = null);

        T Get(Expression<Func<T, bool>> expression,
           List<string>? includes = null);

        void Insert(T entity);

        void Attach(T entity);

        void InsertRange(IEnumerable<T> entities);

        void Delete(object id);

        void Save(T entity);

        public void DeleteByExpression(Expression<Func<T, bool>> expression);

        T Find(params object[] keyValues);
        void DeleteCK(object key1, object key2);

        void UpdateRange(IEnumerable<T> entities);

    }

}
