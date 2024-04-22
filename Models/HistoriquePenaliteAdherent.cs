using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class HistoriquePenaliteAdherent
{
    public string IdAdherent { get; set; } = null!;

    public DateTime DatePenalite { get; set; }

    public decimal? NombreJoursPenalite { get; set; }
}
