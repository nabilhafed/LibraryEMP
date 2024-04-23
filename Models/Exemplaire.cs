using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Exemplaire
{
    public string IdExemplaire { get; set; } = null!;

    public decimal? IdEtat { get; set; }

    public string? Cote { get; set; }

    public virtual EtatExemplaire? IdEtatNavigation { get; set; }
}
