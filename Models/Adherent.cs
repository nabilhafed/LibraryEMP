using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Adherent
{
    public string IdAdherent { get; set; } = null!;

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public decimal? IdPosition { get; set; }

    public string? IdCategorie { get; set; }

    public decimal? EtatAdherent { get; set; }

    public virtual ICollection<HistoriqueAuth> HistoriqueAuths { get; set; } = new List<HistoriqueAuth>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
