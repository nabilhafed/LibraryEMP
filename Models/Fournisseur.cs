using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Fournisseur
{
    public decimal IdFournisseur { get; set; }

    public string? RaisonSociale { get; set; }

    public string? Adresse { get; set; }

    public string? NumTel { get; set; }

    public string? Mail { get; set; }

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
}
