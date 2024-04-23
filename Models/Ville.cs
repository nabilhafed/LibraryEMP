using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Ville
{
    public decimal IdVille { get; set; }

    public string? Ville1 { get; set; }

    public virtual ICollection<NoticeEdition> NoticeEditions { get; set; } = new List<NoticeEdition>();
}
