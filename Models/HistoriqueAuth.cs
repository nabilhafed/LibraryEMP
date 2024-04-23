using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class HistoriqueAuth
{
    public string IdAdmin { get; set; } = null!;

    public DateTime DateOperation { get; set; }

    public string IdAdherent { get; set; } = null!;

    public decimal? IdTypeOperation { get; set; }

    public virtual Adherent IdAdherentNavigation { get; set; } = null!;

    public virtual Operation? IdTypeOperationNavigation { get; set; }
}
