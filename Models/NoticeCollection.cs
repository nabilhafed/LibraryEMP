using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class NoticeCollection
{
    public decimal IdNotice { get; set; }

    public decimal IdCollection { get; set; }

    public string? NumeroDansCollection { get; set; }

    public virtual Collection IdCollectionNavigation { get; set; } = null!;

    public virtual Notice IdNoticeNavigation { get; set; } = null!;
}
