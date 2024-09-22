using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using WebAppCatalogLibrary.DataBase.Models;

namespace WebAppCatalogLibrary.DataBase.Repositories
{
    public abstract class BaseRepository<T>(DbPostgresContext context) : IRepository<T>, IDisposable where T : class, new()
    {
        protected readonly DbPostgresContext _context = context;

        public Task<bool> Create(T value) 
        {
            try
            {
                _context.Add(value);
                return Task.FromResult(_context.SaveChanges() > 0);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
        public Task<bool> Delete(T value) 
        {
            try
            {
                _context.Remove(value);
                return Task.FromResult(_context.SaveChanges() > 0);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
        public Task<bool> Update(T value)
        {
            try
            {
                _context.Update(value);
                return Task.FromResult(_context.SaveChanges() > 0);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }


        public abstract Task<T[]> GetAllAsync();

        public abstract Task<T> GetByIdAsync(Guid id);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
