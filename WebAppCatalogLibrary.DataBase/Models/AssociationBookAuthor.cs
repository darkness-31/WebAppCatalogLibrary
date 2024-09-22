using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAppCatalogLibrary.DataBase.Models;

public partial class AssociationBookAuthor
{
    public Guid AssociationBookAuthorId { get; set; }

    public Guid AuthorId { get; set; }

    public Guid BookId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Author Author { get; set; }

    [JsonIgnore]
    public virtual Book Book { get; set; }
}
