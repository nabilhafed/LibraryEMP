using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Diplome
{
    public decimal IdDiplome { get; set; }

    public string? Diplome1 { get; set; }

    public virtual ICollection<NoticeDipDisEtab> NoticeDipDisEtabs { get; set; } = new List<NoticeDipDisEtab>();
}
