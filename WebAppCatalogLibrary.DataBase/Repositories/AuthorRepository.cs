using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebAppCatalogLibrary.DataBase.Models;

namespace WebAppCatalogLibrary.DataBase.Repositories
{
    public class AuthorRepository(DbPostgresContext context) : BaseRepository<Author>(context)
    {
        public override Task<Author[]> GetAllAsync()
        {
            try
            {
                var data = this._context.Authors.ToArray();
                return Task.FromResult(data);
            }
            catch (Exception)
            {
                return Task.FromResult(Array.Empty<Author>());
            }
        }

        public override Task<Author> GetByIdAsync(Guid id)
        {
            try
            {
                var data = this._context.Authors.FirstOrDefault(x => x.AuthorId == id);
                if (data == null)
                    return Task.FromResult(new Author());
                return Task.FromResult(data);
            }
            catch (Exception)
            {
                return Task.FromResult(new Author());
            }
        }
    }
}
