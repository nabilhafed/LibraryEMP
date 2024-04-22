using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class NoticeTermeExact
{
    public decimal IdTermeExact { get; set; }

    public decimal IdNotice { get; set; }

    public decimal? PoidsTerme { get; set; }
}
