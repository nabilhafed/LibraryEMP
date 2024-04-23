using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class MentionEdition
{
    public decimal IdNotice { get; set; }

    public string? Mention { get; set; }

    public virtual Notice IdNoticeNavigation { get; set; } = null!;
}
