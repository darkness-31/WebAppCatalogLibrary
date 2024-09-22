using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppCatalogLibrary.DataBase.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        public Task<T> GetByIdAsync(Guid id);
        public Task<T[]> GetAllAsync();
        public Task<bool> Create(T value);
        public Task<bool> Update(T value); 
        public Task<bool> Delete(T value);
    }
}
