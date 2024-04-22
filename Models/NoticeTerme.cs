using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class NoticeTerme
{
    public decimal IdTerme { get; set; }

    public decimal IdNotice { get; set; }

    public decimal PoidsTerme { get; set; }

    public virtual Notice IdNoticeNavigation { get; set; } = null!;

    public virtual Terme IdTermeNavigation { get; set; } = null!;
}
