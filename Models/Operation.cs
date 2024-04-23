using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Operation
{
    public decimal IdOperation { get; set; }

    public string? Operation1 { get; set; }

    public virtual ICollection<HistoriqueAuth> HistoriqueAuths { get; set; } = new List<HistoriqueAuth>();
}
