using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class TableCdd
{
    public string Cdd { get; set; } = null!;

    public string? Libelle { get; set; }

    public virtual ICollection<Notice> Notices { get; set; } = new List<Notice>();
}
