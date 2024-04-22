using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Collection
{
    public decimal IdCollection { get; set; }

    public string? TitreCollection { get; set; }

    public string? SousTitreCollection { get; set; }

    public string? IssnCollection { get; set; }

    public virtual ICollection<NoticeCollection> NoticeCollections { get; set; } = new List<NoticeCollection>();

    public virtual ICollection<MentionResponsabilite> IdMentionRes { get; set; } = new List<MentionResponsabilite>();
}
