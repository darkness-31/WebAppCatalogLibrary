using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebAppCatalogLibrary.DataBase.Models;

namespace WebAppCatalogLibrary.DataBase.Repositories
{
    public class AssociationBookAuthorRepository(DbPostgresContext context) : BaseRepository<AssociationBookAuthor>(context)
    {
        public override Task<AssociationBookAuthor[]> GetAllAsync()
        {
            try
            {
                var data = this._context.AssociationBookAuthors.ToArray();
                return Task.FromResult(data);
            }
            catch (Exception)
            {
                return Task.FromResult(Array.Empty<AssociationBookAuthor>());
            }
        }

        public override Task<AssociationBookAuthor> GetByIdAsync(Guid id)
        {
            try
            {
                var data = this._context.AssociationBookAuthors.FirstOrDefault(x => x.BookId == id);
                if (data == null)
                    return Task.FromResult(new AssociationBookAuthor());
                return Task.FromResult(data);
            }
            catch (Exception)
            {
                return Task.FromResult(new AssociationBookAuthor());
            }
        }
    }
}
