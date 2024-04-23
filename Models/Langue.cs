using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Langue
{
    public string IdLangue { get; set; } = null!;

    public string? Langue1 { get; set; }

    public virtual ICollection<Notice> IdNotices { get; set; } = new List<Notice>();
}
