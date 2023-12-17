using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository
{
    public class UnitOfWork<C, T> : IUnitOfWork<T> where T : class
       where C : DbContext
    {
        private readonly C _context;

        private IGenericRepository<T> _repository;

        public bool _isDisposable;

        public UnitOfWork(C context)
        {
            _context = context;
        }
        public IGenericRepository<T> Repository => _repository ??= new GenericRepository<C, T>(_context);

        public void Dispose()
        {
            if (!_isDisposable)
            {
                _context.Dispose();
                GC.SuppressFinalize(this);
            }
            _isDisposable = true;
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }


        ~UnitOfWork()
        {
            if (!_isDisposable)
            {
                Dispose();
            }
            _isDisposable = true;

        }
    }
}
