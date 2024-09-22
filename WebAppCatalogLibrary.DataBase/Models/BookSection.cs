using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAppCatalogLibrary.DataBase.Models;

public partial class BookSection
{
    public Guid BookSectionId { get; set; }

    public string Name { get; set; }

    public DateTime? CreatedOn { get; set; }

    [JsonIgnore]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
