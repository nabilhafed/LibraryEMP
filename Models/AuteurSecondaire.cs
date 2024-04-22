using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class AuteurSecondaire
{
    public decimal IdNotice { get; set; }

    public decimal IdMentionRes { get; set; }

    public decimal IdFonction { get; set; }

    public virtual Fonction IdFonctionNavigation { get; set; } = null!;

    public virtual MentionResponsabilite IdMentionResNavigation { get; set; } = null!;

    public virtual Notice IdNoticeNavigation { get; set; } = null!;
}
