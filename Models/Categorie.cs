using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Categorie
{
    public string IdCategorie { get; set; } = null!;

    public string? LibelleCategorie { get; set; }

    public decimal? NombreDocument { get; set; }

    public decimal? DureePret { get; set; }
}
