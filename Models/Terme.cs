using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Terme
{
    public decimal IdTerme { get; set; }

    public string? Terme1 { get; set; }

    public virtual ICollection<NoticeTerme> NoticeTermes { get; set; } = new List<NoticeTerme>();
}
