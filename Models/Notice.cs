using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class Notice
{
    public decimal IdNotice { get; set; }

    public decimal? IdSourceArticle { get; set; }

    public decimal IdType { get; set; }

    public string? IdPeriodicite { get; set; }

    public string? TitrePropre { get; set; }

    public string? SousTitre { get; set; }

    public string? NPartie { get; set; }

    public string? CollationImpMaterielle { get; set; }

    public string? CollationAutresCarMat { get; set; }

    public string? CollationFormat { get; set; }

    public decimal? NbrExemple { get; set; }

    public string? Cote { get; set; }

    public string? Localisation { get; set; }

    public string? Cdd { get; set; }

    public string? Resume { get; set; }

    public string? Isbn { get; set; }

    public string? Date1erPub { get; set; }

    public string? TitreCle { get; set; }

    public string? NumeroVol { get; set; }

    public string? IssnNotice { get; set; }

    public string? NoteGenerale { get; set; }

    public decimal? IsIndexed { get; set; }

    public string? Accessibilite { get; set; }

    public decimal? ExemplaireExiste { get; set; }

    public string? TypeDonneesResourceElec { get; set; }

    public virtual ICollection<AuteurSecondaire> AuteurSecondaires { get; set; } = new List<AuteurSecondaire>();

    public virtual TableCdd? CddNavigation { get; set; }

    public virtual Periodicite? IdPeriodiciteNavigation { get; set; }

    public virtual SourceArticle? IdSourceArticleNavigation { get; set; }

    public virtual TypeNotice IdTypeNavigation { get; set; } = null!;

    public virtual MentionEdition? MentionEdition { get; set; }

    public virtual ICollection<NoticeCollection> NoticeCollections { get; set; } = new List<NoticeCollection>();

    public virtual NoticeDipDisEtab? NoticeDipDisEtab { get; set; }

    public virtual ICollection<NoticeEdition> NoticeEditions { get; set; } = new List<NoticeEdition>();

    public virtual ICollection<NoticeTerme> NoticeTermes { get; set; } = new List<NoticeTerme>();

    public virtual ICollection<Langue> IdLangues { get; set; } = new List<Langue>();

    public virtual ICollection<MentionResponsabilite> IdMentionRes { get; set; } = new List<MentionResponsabilite>();

    public virtual ICollection<MentionResponsabilite> IdMentionResNavigation { get; set; } = new List<MentionResponsabilite>();

    public virtual ICollection<MotsCle> IdMotCles { get; set; } = new List<MotsCle>();

    public virtual ICollection<Pay> IdPays { get; set; } = new List<Pay>();

    public virtual ICollection<Selection> IdSelections { get; set; } = new List<Selection>();
}
