using System;
using System.Collections.Generic;

namespace LibraryEMP.Models;

public partial class NoticeDipDisEtab
{
    public decimal IdDiplome { get; set; }

    public decimal IdEtablissement { get; set; }

    public decimal IdDiscipline { get; set; }

    public decimal IdNotice { get; set; }

    public string? NoteTexte { get; set; }

    public string? AnneSoutenance { get; set; }

    public virtual Diplome IdDiplomeNavigation { get; set; } = null!;

    public virtual Discipline IdDisciplineNavigation { get; set; } = null!;

    public virtual Etablissement IdEtablissementNavigation { get; set; } = null!;

    public virtual Notice IdNoticeNavigation { get; set; } = null!;
}
