using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAppCatalogLibrary.DataBase.Models;

public partial class Author
{
    public Guid AuthorId { get; set; }

    public string FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string SecondName { get; set; }

    public DateTime? CreatedOn { get; set; }

    [JsonIgnore]
    public virtual ICollection<AssociationBookAuthor> AssociationBookAuthors { get; set; } = new List<AssociationBookAuthor>();
}
