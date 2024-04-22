using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Pay
{
    public string IdPays { get; set; } = null!;

    public string? Pays { get; set; }

    public virtual ICollection<Notice> IdNotices { get; set; } = new List<Notice>();
}
