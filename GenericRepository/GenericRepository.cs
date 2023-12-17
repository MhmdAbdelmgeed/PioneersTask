using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GenericRepository
{
    public class GenericRepository<C, T> : IGenericRepository<T> where T : class
     where C : DbContext
    {

        private readonly C _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(C context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, List<string>? includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _context.ChangeTracker.Clear();
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IList<T> GetAll(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.AsNoTracking().ToList();
        }

        public IQueryable<T> GetAllIQueryable(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }


        public T Get(Expression<Func<T, bool>> expression, List<string>? includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.AsNoTracking().FirstOrDefault(expression);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Attach(T entity)
        {
            _dbSet.Attach(entity);
        }

        public void Save(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void DeleteByExpression(Expression<Func<T, bool>> expression)
        {
            var entity = _dbSet.FirstOrDefault(expression);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public T Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);

        }
        public void DeleteCK(object key1, object key2)
        {
            var entity = _dbSet.Find(key1, key2);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.ChangeTracker.Clear();
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }

}
