using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Periodicite
{
    public string IdPeriodicite { get; set; } = null!;

    public string? Periodicite1 { get; set; }

    public virtual ICollection<Notice> Notices { get; set; } = new List<Notice>();
}
