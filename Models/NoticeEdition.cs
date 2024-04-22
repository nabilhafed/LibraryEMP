using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class NoticeEdition
{
    public decimal IdVille { get; set; }

    public decimal IdEditeur { get; set; }

    public decimal IdNotice { get; set; }

    public string? DateEdition { get; set; }

    public virtual Editeur IdEditeurNavigation { get; set; } = null!;

    public virtual Notice IdNoticeNavigation { get; set; } = null!;

    public virtual Ville IdVilleNavigation { get; set; } = null!;
}
