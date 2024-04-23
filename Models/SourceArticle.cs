using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class SourceArticle
{
    public decimal IdSourceArticle { get; set; }

    public string? TitreSourceArticle { get; set; }

    public string? DatePubArticle { get; set; }

    public string? IntervalePage { get; set; }

    public string? NumeroRevue { get; set; }

    public string? IssnRevue { get; set; }

    public virtual ICollection<Notice> Notices { get; set; } = new List<Notice>();
}
