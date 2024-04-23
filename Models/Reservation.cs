using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Reservation
{
    public string IdAdherent { get; set; } = null!;

    public string Cote { get; set; } = null!;

    public DateTime HeureReservation { get; set; }

    public virtual Adherent IdAdherentNavigation { get; set; } = null!;
}
