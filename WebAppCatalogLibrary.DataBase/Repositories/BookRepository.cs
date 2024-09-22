using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebAppCatalogLibrary.DataBase.Models;

namespace WebAppCatalogLibrary.DataBase.Repositories
{
    public class BookRepository(DbPostgresContext context) : BaseRepository<Book>(context)
    {
        public override Task<Book[]> GetAllAsync()
        {
            try
            {
                var data = this._context.Books.ToArray();
                return Task.FromResult(data);
            }
            catch (Exception)
            {
                return Task.FromResult(Array.Empty<Book>());
            }  
        }

        public override Task<Book> GetByIdAsync(Guid id)
        {
            try
            {
                var data = this._context.Books.FirstOrDefault(x => x.BookId == id);
                if (data == null)
                    return Task.FromResult(new Book());
                return Task.FromResult(data);
            }
            catch (Exception)
            {
                return Task.FromResult(new Book());
            }
        }
    }
}
