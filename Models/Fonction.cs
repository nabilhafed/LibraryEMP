using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Fonction
{
    public decimal IdFonction { get; set; }

    public string? Fonction1 { get; set; }

    public virtual ICollection<AuteurSecondaire> AuteurSecondaires { get; set; } = new List<AuteurSecondaire>();
}
