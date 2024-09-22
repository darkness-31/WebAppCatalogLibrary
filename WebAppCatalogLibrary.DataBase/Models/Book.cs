using System;
using System.Collections.Generic;

namespace WebAppCatalogLibrary.DataBase.Models;

public partial class Book
{
    public Guid BookId { get; set; }

    public string Title { get; set; }

    public int? Tom { get; set; }

    public string Location { get; set; }

    public Guid BookSectionId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public ICollection<AssociationBookAuthor> AssociationBookAuthors { get; set; } = new List<AssociationBookAuthor>();

    public BookSection BookSection { get; set; }
}
