using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;

using System.Text.Json;
using System.Text.Json.Serialization;
using WebAppCatalogLibrary.DataBase.Models;
using WebAppCatalogLibrary.DataBase.Repositories;

namespace WebAppCatalogLibrary.Pages
{
    public class IndexModel(ILogger<IndexModel> logger, DbPostgresContext db) : PageModel
    {

        private readonly ILogger<IndexModel> _logger = logger;
        private readonly IRepository<Book> _repBook = new BookRepository(db);
        private readonly IRepository<Author> _repAuthor = new AuthorRepository(db);
        private readonly IRepository<BookSection> _repSection = new SectionRepository(db);
        private readonly IRepository<AssociationBookAuthor> _repABookAuthor = new AssociationBookAuthorRepository(db);

        public IEnumerable<Book> Books { get; set; } = [];
        public IEnumerable<Author> Authors { get; set; } = [];
        public IEnumerable<BookSection> BookSections { get; set; } = [];
        public IEnumerable<AssociationBookAuthor> Associations { get; set; } = [];

        public HtmlString BooksJsonSerialization { get; set; } = new HtmlString(string.Empty);

        public async void OnGet()
        {
            Books = await _repBook.GetAllAsync();
            Authors = await _repAuthor.GetAllAsync();
            BookSections = await _repSection.GetAllAsync();
            Associations = await _repABookAuthor.GetAllAsync();

            BooksJsonSerialization = new HtmlString(JsonSerializer.Serialize(Books.ToArray()));
        }

        public IActionResult OnPostByCreate(OnPostConfigModel config)
        {
            var idbook = Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(config.Title) || 
                config.Author == null || 
                config.Section == null)
            {
                return RedirectToPage("Index");
            }


            var book = new Book()
            {
                BookId = idbook,
                Title = config.Title,
                Tom = config.Tom,
                Location = config.Location!,
                BookSectionId = (Guid)config.Section
            };
            _repBook.Create(book);

            foreach (var item in config.Author)
            {
                var aBookAuthor = new AssociationBookAuthor()
                {
                    BookId = idbook,
                    AuthorId = item
                };

                _repABookAuthor.Create(aBookAuthor);        
            }

            return RedirectToPage("Index");
        }

        public IActionResult OnPostByDelete(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return RedirectToPage("Index");


            var book = JsonSerializer.Deserialize<Book>(json);
            if (book != null)
                _repBook.Delete(book);

            return RedirectToPage("Index");
        }

        public IActionResult OnPostByUpdate(OnPostConfigModel config, string json)
        {
            if (string.IsNullOrWhiteSpace(json) || 
                string.IsNullOrWhiteSpace(config.Title) || 
                config.Section == null ||
                config.Author == null ||
                config.Author.Length <= 0)
            {
                return RedirectToPage("Index");
            }

            var book = JsonSerializer.Deserialize<Book>(json);

            if (book == null)
                return RedirectToPage("Index");
            
            if (config.Title != book.Title)
                book.Title = config.Title;
            if (config.Tom != book.Tom)
                book.Tom = book.Tom;
            if (config.Section != book.BookSectionId)
                book.BookSectionId = (Guid)config.Section;
            if (config.Location != book.Location)
                book.Location = config.Location ?? string.Empty;
            if (config.Author != null)
            {
                foreach (var item in book.AssociationBookAuthors)
                {
                    if (!config.Author.Contains(item.AuthorId))
                    {
                        _repABookAuthor.Delete(item);
                    }
                }

                var authors = config.Author.Except(book.AssociationBookAuthors.Select(x => x.AuthorId));
                foreach (var item in authors)
                {
                    var aBookAuthor = new AssociationBookAuthor()
                    {
                        AuthorId = item,
                        BookId = book.BookId
                    };
                    _repABookAuthor.Create(aBookAuthor);
                }
            }

            _repBook.Update(book);
            return RedirectToPage("Index");
        }
    }

    public record OnPostConfigModel(string? Title, int? Tom, Guid[]? Author, Guid? Section, string? Location);
}
