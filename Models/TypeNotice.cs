using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class TypeNotice
{
    public decimal IdType { get; set; }

    public string? TypeNotice1 { get; set; }

    public virtual ICollection<Notice> Notices { get; set; } = new List<Notice>();
}
