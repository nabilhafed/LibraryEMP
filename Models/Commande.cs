using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Commande
{
    public string NumCommande { get; set; } = null!;

    public string? Observation { get; set; }

    public DateTime? DateReception { get; set; }

    public string? MontantGlobal { get; set; }

    public decimal? IdFournisseur { get; set; }

    public virtual Fournisseur? IdFournisseurNavigation { get; set; }
}
