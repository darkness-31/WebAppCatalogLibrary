using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebAppCatalogLibrary.DataBase.Models;

namespace WebAppCatalogLibrary.DataBase.Repositories
{
    public class SectionRepository(DbPostgresContext context) : BaseRepository<BookSection>(context)
    {
        public override Task<BookSection[]> GetAllAsync()
        {
            try
            {
                var data = this._context.BookSections.ToArray();
                return Task.FromResult(data);
            }
            catch (Exception)
            {
                return Task.FromResult(Array.Empty<BookSection>());
            }
        }

        public override Task<BookSection> GetByIdAsync(Guid id)
        {
            try
            {
                var data = this._context.BookSections.FirstOrDefault(x => x.BookSectionId == id);
                if (data == null)
                    return Task.FromResult(new BookSection());
                return Task.FromResult(data);
            }
            catch (Exception)
            {
                return Task.FromResult(new BookSection());
            }
        }
    }
}
