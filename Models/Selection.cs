using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Selection
{
    public decimal IdSelection { get; set; }

    public string? LibelleSelection { get; set; }

    public virtual ICollection<Notice> IdNotices { get; set; } = new List<Notice>();
}
