using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryEMP.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adherent> Adherents { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Aquisition> Aquisitions { get; set; }

    public virtual DbSet<AuteurSecondaire> AuteurSecondaires { get; set; }

    public virtual DbSet<Categorie> Categories { get; set; }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<Commande> Commandes { get; set; }

    public virtual DbSet<CopieHistoPenaliteAdherent> CopieHistoPenaliteAdherents { get; set; }

    public virtual DbSet<Diplome> Diplomes { get; set; }

    public virtual DbSet<Discipline> Disciplines { get; set; }

    public virtual DbSet<Editeur> Editeurs { get; set; }

    public virtual DbSet<Etablissement> Etablissements { get; set; }

    public virtual DbSet<EtatAdherent> EtatAdherents { get; set; }

    public virtual DbSet<EtatExemplaire> EtatExemplaires { get; set; }

    public virtual DbSet<Exemplaire> Exemplaires { get; set; }

    public virtual DbSet<Fonction> Fonctions { get; set; }

    public virtual DbSet<Fournisseur> Fournisseurs { get; set; }

    public virtual DbSet<HistoriqueAuth> HistoriqueAuths { get; set; }

    public virtual DbSet<HistoriquePenaliteAdherent> HistoriquePenaliteAdherents { get; set; }

    public virtual DbSet<HistoriquePret> HistoriquePrets { get; set; }

    public virtual DbSet<JoursFery> JoursFeries { get; set; }

    public virtual DbSet<Langue> Langues { get; set; }

    public virtual DbSet<MentionEdition> MentionEditions { get; set; }

    public virtual DbSet<MentionResponsabilite> MentionResponsabilites { get; set; }

    public virtual DbSet<MotsCle> MotsCles { get; set; }

    public virtual DbSet<MotsVide> MotsVides { get; set; }

    public virtual DbSet<Newacqui> Newacquis { get; set; }

    public virtual DbSet<Notice> Notices { get; set; }

    public virtual DbSet<NoticeCollection> NoticeCollections { get; set; }

    public virtual DbSet<NoticeDipDisEtab> NoticeDipDisEtabs { get; set; }

    public virtual DbSet<NoticeEdition> NoticeEditions { get; set; }

    public virtual DbSet<NoticeMentionEdition> NoticeMentionEditions { get; set; }

    public virtual DbSet<NoticeTerme> NoticeTermes { get; set; }

    public virtual DbSet<NoticeTermeExact> NoticeTermeExacts { get; set; }

    public virtual DbSet<NoticeTheme> NoticeThemes { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<ParametresCatlibPret> ParametresCatlibPrets { get; set; }

    public virtual DbSet<Pay> Pays { get; set; }

    public virtual DbSet<Penalite> Penalites { get; set; }

    public virtual DbSet<PenaliteAdherent> PenaliteAdherents { get; set; }

    public virtual DbSet<PenaliteAdherentTemp> PenaliteAdherentTemps { get; set; }

    public virtual DbSet<Periodicite> Periodicites { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Pret> Prets { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Selection> Selections { get; set; }

    public virtual DbSet<SourceArticle> SourceArticles { get; set; }

    public virtual DbSet<TableCdd> TableCdds { get; set; }

    public virtual DbSet<Terme> Termes { get; set; }

    public virtual DbSet<TermeExact> TermeExacts { get; set; }

    public virtual DbSet<Theme> Themes { get; set; }

    public virtual DbSet<TypeNotice> TypeNotices { get; set; }

    public virtual DbSet<Ville> Villes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=EMPLIBRARY)));User ID=mataoui;Password=123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("MATAOUI")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Adherent>(entity =>
        {
            entity.HasKey(e => e.IdAdherent).HasName("ADHERENT_PK");

            entity.ToTable("ADHERENT");

            entity.Property(e => e.IdAdherent)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID_ADHERENT");
            entity.Property(e => e.EtatAdherent)
                .HasColumnType("NUMBER")
                .HasColumnName("ETAT_ADHERENT");
            entity.Property(e => e.IdCategorie)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ID_CATEGORIE");
            entity.Property(e => e.IdPosition)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_POSITION");
            entity.Property(e => e.Nom)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("NOM");
            entity.Property(e => e.Prenom)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("PRENOM");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.IdAdmin).HasName("ADMIN_PK");

            entity.ToTable("ADMIN");

            entity.Property(e => e.IdAdmin)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID_ADMIN");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
        });

        modelBuilder.Entity<Aquisition>(entity =>
        {
            entity.HasKey(e => new { e.NumCommande, e.IdExemplaire }).HasName("AQUISITION_PK");

            entity.ToTable("AQUISITION");

            entity.Property(e => e.NumCommande)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("NUM_COMMANDE");
            entity.Property(e => e.IdExemplaire)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID_EXEMPLAIRE");
            entity.Property(e => e.PrixUnitaire)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PRIX_UNITAIRE");
        });

        modelBuilder.Entity<AuteurSecondaire>(entity =>
        {
            entity.HasKey(e => new { e.IdNotice, e.IdMentionRes, e.IdFonction });

            entity.ToTable("AUTEUR_SECONDAIRE");

            entity.Property(e => e.IdNotice)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_NOTICE");
            entity.Property(e => e.IdMentionRes)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_MENTION_RES");
            entity.Property(e => e.IdFonction)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_FONCTION");

            entity.HasOne(d => d.IdFonctionNavigation).WithMany(p => p.AuteurSecondaires)
                .HasForeignKey(d => d.IdFonction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AUTEUR_S_LIEN_161_FONCTION");

            entity.HasOne(d => d.IdMentionResNavigation).WithMany(p => p.AuteurSecondaires)
                .HasForeignKey(d => d.IdMentionRes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AUTEUR_S_LIEN_143_MENTION_");

            entity.HasOne(d => d.IdNoticeNavigation).WithMany(p => p.AuteurSecondaires)
                .HasForeignKey(d => d.IdNotice)
                .HasConstraintName("FK_AUTEUR_S_LIEN_138_NOTICE");
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.IdCategorie).HasName("CATEGORIE_PK");

            entity.ToTable("CATEGORIE");

            entity.Property(e => e.IdCategorie)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID_CATEGORIE");
            entity.Property(e => e.DureePret)
                .HasColumnType("NUMBER")
                .HasColumnName("DUREE_PRET");
            entity.Property(e => e.LibelleCategorie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LIBELLE_CATEGORIE");
            entity.Property(e => e.NombreDocument)
                .HasColumnType("NUMBER")
                .HasColumnName("NOMBRE_DOCUMENT");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.IdCollection);

            entity.ToTable("COLLECTION");

            entity.Property(e => e.IdCollection)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_COLLECTION");
            entity.Property(e => e.IssnCollection)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ISSN_COLLECTION");
            entity.Property(e => e.SousTitreCollection)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SOUS_TITRE_COLLECTION");
            entity.Property(e => e.TitreCollection)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITRE_COLLECTION");

            entity.HasMany(d => d.IdMentionRes).WithMany(p => p.IdCollections)
                .UsingEntity<Dictionary<string, object>>(
                    "MentionResCollection",
                    r => r.HasOne<MentionResponsabilite>().WithMany()
                        .HasForeignKey("IdMentionRes")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MENTION__LIEN_164_MENTION_"),
                    l => l.HasOne<Collection>().WithMany()
                        .HasForeignKey("IdCollection")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MENTION__LIEN_163_COLLECTI"),
                    j =>
                    {
                        j.HasKey("IdCollection", "IdMentionRes");
                        j.ToTable("MENTION_RES_COLLECTION");
                        j.IndexerProperty<decimal>("IdCollection")
                            .HasColumnType("NUMBER(38)")
                            .HasColumnName("ID_COLLECTION");
                        j.IndexerProperty<decimal>("IdMentionRes")
                            .HasColumnType("NUMBER(38)")
                            .HasColumnName("ID_MENTION_RES");
                    });
        });

        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasKey(e => e.NumCommande).HasName("COMMANDE_PK");

            entity.ToTable("COMMANDE");

            entity.Property(e => e.NumCommande)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("NUM_COMMANDE");
            entity.Property(e => e.DateReception)
                .HasColumnType("DATE")
                .HasColumnName("DATE_RECEPTION");
            entity.Property(e => e.IdFournisseur)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_FOURNISSEUR");
            entity.Property(e => e.MontantGlobal)
                .IsUnicode(false)
                .HasColumnName("MONTANT_GLOBAL");
            entity.Property(e => e.Observation)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("OBSERVATION");

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.Commandes)
                .HasForeignKey(d => d.IdFournisseur)
                .HasConstraintName("COMMANDE_FOURNISSEUR_FK1");
        });

        modelBuilder.Entity<CopieHistoPenaliteAdherent>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("COPIE_HISTO_PENALITE_ADHERENT");

            entity.Property(e => e.DatePenalite)
                .HasColumnType("DATE")
                .HasColumnName("DATE_PENALITE");
            entity.Property(e => e.IdAdherent)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID_ADHERENT");
            entity.Property(e => e.NombreJoursPenalite)
                .HasColumnType("NUMBER")
                .HasColumnName("NOMBRE_JOURS_PENALITE");
        });

        modelBuilder.Entity<Diplome>(entity =>
        {
            entity.HasKey(e => e.IdDiplome);

            entity.ToTable("DIPLOME");

            entity.Property(e => e.IdDiplome)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_DIPLOME");
            entity.Property(e => e.Diplome1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DIPLOME");
        });

        modelBuilder.Entity<Discipline>(entity =>
        {
            entity.HasKey(e => e.IdDiscipline);

            entity.ToTable("DISCIPLINE");

            entity.Property(e => e.IdDiscipline)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_DISCIPLINE");
            entity.Property(e => e.Discipline1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DISCIPLINE");
        });

        modelBuilder.Entity<Editeur>(entity =>
        {
            entity.HasKey(e => e.IdEditeur);

            entity.ToTable("EDITEUR");

            entity.Property(e => e.IdEditeur)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_EDITEUR");
            entity.Property(e => e.Editeur1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EDITEUR");
        });

        modelBuilder.Entity<Etablissement>(entity =>
        {
            entity.HasKey(e => e.IdEtablissement);

            entity.ToTable("ETABLISSEMENT");

            entity.Property(e => e.IdEtablissement)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_ETABLISSEMENT");
            entity.Property(e => e.Etablissement1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ETABLISSEMENT");
        });

        modelBuilder.Entity<EtatAdherent>(entity =>
        {
            entity.HasKey(e => e.IdEtat).HasName("ETAT_ADHERENT_PK");

            entity.ToTable("ETAT_ADHERENT");

            entity.Property(e => e.IdEtat)
                .HasPrecision(1)
                .HasColumnName("ID_ETAT");
            entity.Property(e => e.DescEtat)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("DESC_ETAT");
        });

        modelBuilder.Entity<EtatExemplaire>(entity =>
        {
            entity.HasKey(e => e.IdEtat).HasName("ETAT_EXEMPLAIRE_PK");

            entity.ToTable("ETAT_EXEMPLAIRE");

            entity.Property(e => e.IdEtat)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_ETAT");
            entity.Property(e => e.LibelleEtat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LIBELLE_ETAT");
        });

        modelBuilder.Entity<Exemplaire>(entity =>
        {
            entity.HasKey(e => e.IdExemplaire).HasName("EXEMPLAIRE_PK");

            entity.ToTable("EXEMPLAIRE");

            entity.Property(e => e.IdExemplaire)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID_EXEMPLAIRE");
            entity.Property(e => e.Cote)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("COTE");
            entity.Property(e => e.IdEtat)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_ETAT");

            entity.HasOne(d => d.IdEtatNavigation).WithMany(p => p.Exemplaires)
                .HasForeignKey(d => d.IdEtat)
                .HasConstraintName("EXEMPLAIRE_ETAT_EXEMPLAIR_FK1");
        });

        modelBuilder.Entity<Fonction>(entity =>
        {
            entity.HasKey(e => e.IdFonction);

            entity.ToTable("FONCTION");

            entity.Property(e => e.IdFonction)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_FONCTION");
            entity.Property(e => e.Fonction1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FONCTION");
        });

        modelBuilder.Entity<Fournisseur>(entity =>
        {
            entity.HasKey(e => e.IdFournisseur).HasName("FOURNISSEUR_PK");

            entity.ToTable("FOURNISSEUR");

            entity.Property(e => e.IdFournisseur)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_FOURNISSEUR");
            entity.Property(e => e.Adresse)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("ADRESSE");
            entity.Property(e => e.Mail)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("MAIL");
            entity.Property(e => e.NumTel)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("NUM_TEL");
            entity.Property(e => e.RaisonSociale)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("RAISON_SOCIALE");
        });

        modelBuilder.Entity<HistoriqueAuth>(entity =>
        {
            entity.HasKey(e => new { e.IdAdmin, e.DateOperation, e.IdAdherent }).HasName("HISTORIQUE_AUTH_PK");

            entity.ToTable("HISTORIQUE_AUTH");

            entity.Property(e => e.IdAdmin)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID_ADMIN");
            entity.Property(e => e.DateOperation)
                .HasPrecision(6)
                .HasColumnName("DATE_OPERATION");
            entity.Property(e => e.IdAdherent)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID_ADHERENT");
            entity.Property(e => e.IdTypeOperation)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_TYPE_OPERATION");

            entity.HasOne(d => d.IdAdherentNavigation).WithMany(p => p.HistoriqueAuths)
                .HasForeignKey(d => d.IdAdherent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("HISTORIQUE_AUTH_ADHERENT_FK1");

            entity.HasOne(d => d.IdTypeOperationNavigation).WithMany(p => p.HistoriqueAuths)
                .HasForeignKey(d => d.IdTypeOperation)
                .HasConstraintName("HISTORIQUE_AUTH_OPERATION_FK1");
        });

        modelBuilder.Entity<HistoriquePenaliteAdherent>(entity =>
        {
            entity.HasKey(e => new { e.IdAdherent, e.DatePenalite }).HasName("HISTORIQUE_PENALITE_ADHER_PK");

            entity.ToTable("HISTORIQUE_PENALITE_ADHERENT");

            entity.Property(e => e.IdAdherent)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID_ADHERENT");
            entity.Property(e => e.DatePenalite)
                .HasColumnType("DATE")
                .HasColumnName("DATE_PENALITE");
            entity.Property(e => e.NombreJoursPenalite)
                .HasColumnType("NUMBER")
                .HasColumnName("NOMBRE_JOURS_PENALITE");
        });

        modelBuilder.Entity<HistoriquePret>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("HISTORIQUE_PRET");

            entity.Property(e => e.DatePret)
                .HasColumnType("DATE")
                .HasColumnName("DATE_PRET");
            entity.Property(e => e.DateRetour)
                .HasColumnType("DATE")
                .HasColumnName("DATE_RETOUR");
            entity.Property(e => e.IdAdherent)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID_ADHERENT");
            entity.Property(e => e.IdExemplaire)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID_EXEMPLAIRE");
        });

        modelBuilder.Entity<JoursFery>(entity =>
        {
            entity.HasKey(e => e.DateJourFerie).HasName("JOURS_FERIES_PK");

            entity.ToTable("JOURS_FERIES");

            entity.Property(e => e.DateJourFerie)
                .HasColumnType("DATE")
                .HasColumnName("DATE_JOUR_FERIE");
        });

        modelBuilder.Entity<Langue>(entity =>
        {
            entity.HasKey(e => e.IdLangue);

            entity.ToTable("LANGUE");

            entity.Property(e => e.IdLangue)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ID_LANGUE");
            entity.Property(e => e.Langue1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LANGUE");
        });

        modelBuilder.Entity<MentionEdition>(entity =>
        {
            entity.HasKey(e => e.IdNotice).HasName("MENTION_EDITION_PK");

            entity.ToTable("MENTION_EDITION");

            entity.Property(e => e.IdNotice)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_NOTICE");
            entity.Property(e => e.Mention)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MENTION");

            entity.HasOne(d => d.IdNoticeNavigation).WithOne(p => p.MentionEdition)
                .HasForeignKey<MentionEdition>(d => d.IdNotice)
                .HasConstraintName("MENTION_EDITION_NOTICE_FK1");
        });

        modelBuilder.Entity<MentionResponsabilite>(entity =>
        {
            entity.HasKey(e => e.IdMentionRes);

            entity.ToTable("MENTION_RESPONSABILITE");

            entity.Property(e => e.IdMentionRes)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_MENTION_RES");
            entity.Property(e => e.AutrePartie)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("AUTRE_PARTIE");
            entity.Property(e => e.Collectivite)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMBER")
                .HasColumnName("COLLECTIVITE");
            entity.Property(e => e.Nom)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<MotsCle>(entity =>
        {
            entity.HasKey(e => e.IdMotCle);

            entity.ToTable("MOTS_CLES");

            entity.Property(e => e.IdMotCle)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_MOT_CLE");
            entity.Property(e => e.IsIndexed)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("IS_INDEXED");
            entity.Property(e => e.MotCle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MOT_CLE");
        });

        modelBuilder.Entity<MotsVide>(entity =>
        {
            entity.HasKey(e => e.MotVide);

            entity.ToTable("MOTS_VIDES");

            entity.Property(e => e.MotVide)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MOT_VIDE");
        });

        modelBuilder.Entity<Newacqui>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("NEWACQUIS");

            entity.Property(e => e.IdNotice)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_NOTICE");
        });

        modelBuilder.Entity<Notice>(entity =>
        {
            entity.HasKey(e => e.IdNotice);

            entity.ToTable("NOTICE");

            entity.Property(e => e.IdNotice)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_NOTICE");
            entity.Property(e => e.Accessibilite)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("1")
                .HasColumnName("ACCESSIBILITE");
            entity.Property(e => e.Cdd)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("CDD");
            entity.Property(e => e.CollationAutresCarMat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COLLATION_AUTRES_CAR_MAT");
            entity.Property(e => e.CollationFormat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COLLATION_FORMAT");
            entity.Property(e => e.CollationImpMaterielle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COLLATION_IMP_MATERIELLE");
            entity.Property(e => e.Cote)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("COTE");
            entity.Property(e => e.Date1erPub)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DATE_1ER_PUB");
            entity.Property(e => e.ExemplaireExiste)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMBER")
                .HasColumnName("EXEMPLAIRE_EXISTE");
            entity.Property(e => e.IdPeriodicite)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("ID_PERIODICITE");
            entity.Property(e => e.IdSourceArticle)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_SOURCE_ARTICLE");
            entity.Property(e => e.IdType)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_TYPE");
            entity.Property(e => e.IsIndexed)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("IS_INDEXED");
            entity.Property(e => e.Isbn)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.IssnNotice)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ISSN_NOTICE");
            entity.Property(e => e.Localisation)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LOCALISATION");
            entity.Property(e => e.NPartie)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("N_PARTIE");
            entity.Property(e => e.NbrExemple)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("NBR_EXEMPLE");
            entity.Property(e => e.NoteGenerale)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOTE_GENERALE");
            entity.Property(e => e.NumeroVol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMERO_VOL");
            entity.Property(e => e.Resume)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("RESUME");
            entity.Property(e => e.SousTitre)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("SOUS_TITRE");
            entity.Property(e => e.TitreCle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITRE_CLE");
            entity.Property(e => e.TitrePropre)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("TITRE_PROPRE");
            entity.Property(e => e.TypeDonneesResourceElec)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("TYPE_DONNEES_RESOURCE_ELEC");

            entity.HasOne(d => d.CddNavigation).WithMany(p => p.Notices)
                .HasForeignKey(d => d.Cdd)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("NOTICE_TABLE_CDD_FK1");

            entity.HasOne(d => d.IdPeriodiciteNavigation).WithMany(p => p.Notices)
                .HasForeignKey(d => d.IdPeriodicite)
                .HasConstraintName("FK_NOTICE_ASSOC_513_PERIODIC");

            entity.HasOne(d => d.IdSourceArticleNavigation).WithMany(p => p.Notices)
                .HasForeignKey(d => d.IdSourceArticle)
                .HasConstraintName("FK_NOTICE_NOTICE_SO_SOURCE_A");

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.Notices)
                .HasForeignKey(d => d.IdType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTICE_ASSOC_379_TYPE_NOT");

            entity.HasMany(d => d.IdLangues).WithMany(p => p.IdNotices)
                .UsingEntity<Dictionary<string, object>>(
                    "NoticeLangue",
                    r => r.HasOne<Langue>().WithMany()
                        .HasForeignKey("IdLangue")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_NOTICE_L_LIEN_153_LANGUE"),
                    l => l.HasOne<Notice>().WithMany()
                        .HasForeignKey("IdNotice")
                        .HasConstraintName("FK_NOTICE_L_LIEN_152_NOTICE"),
                    j =>
                    {
                        j.HasKey("IdNotice", "IdLangue");
                        j.ToTable("NOTICE_LANGUE");
                        j.IndexerProperty<decimal>("IdNotice")
                            .HasColumnType("NUMBER(38)")
                            .HasColumnName("ID_NOTICE");
                        j.IndexerProperty<string>("IdLangue")
                            .HasMaxLength(3)
                            .IsUnicode(false)
                            .HasColumnName("ID_LANGUE");
                    });

            entity.HasMany(d => d.IdMentionRes).WithMany(p => p.IdNotices)
                .UsingEntity<Dictionary<string, object>>(
                    "Auteur",
                    r => r.HasOne<MentionResponsabilite>().WithMany()
                        .HasForeignKey("IdMentionRes")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AUTEUR_LIEN_141_MENTION_"),
                    l => l.HasOne<Notice>().WithMany()
                        .HasForeignKey("IdNotice")
                        .HasConstraintName("FK_AUTEUR_LIEN_140_NOTICE"),
                    j =>
                    {
                        j.HasKey("IdNotice", "IdMentionRes");
                        j.ToTable("AUTEUR");
                        j.IndexerProperty<decimal>("IdNotice")
                            .HasColumnType("NUMBER(38)")
                            .HasColumnName("ID_NOTICE");
                        j.IndexerProperty<decimal>("IdMentionRes")
                            .HasColumnType("NUMBER(38)")
                            .HasColumnName("ID_MENTION_RES");
                    });

            entity.HasMany(d => d.IdMentionResNavigation).WithMany(p => p.IdNoticesNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "CoAuteur",
                    r => r.HasOne<MentionResponsabilite>().WithMany()
                        .HasForeignKey("IdMentionRes")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CO_AUTEU_LIEN_142_MENTION_"),
                    l => l.HasOne<Notice>().WithMany()
                        .HasForeignKey("IdNotice")
                        .HasConstraintName("FK_CO_AUTEU_LIEN_139_NOTICE"),
                    j =>
                    {
                        j.HasKey("IdNotice", "IdMentionRes");
                        j.ToTable("CO_AUTEUR");
                        j.IndexerProperty<decimal>("IdNotice")
                            .HasColumnType("NUMBER(38)")
                            .HasColumnName("ID_NOTICE");
                        j.IndexerProperty<decimal>("IdMentionRes")
                            .HasColumnType("NUMBER(38)")
                            .HasColumnName("ID_MENTION_RES");
                    });

            entity.HasMany(d => d.IdMotCles).WithMany(p => p.IdNotices)
                .UsingEntity<Dictionary<string, object>>(
                    "NoticeMotCle",
                    r => r.HasOne<MotsCle>().WithMany()
                        .HasForeignKey("IdMotCle")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_NOTICE_M_LIEN_160_MOTS_CLE"),
                    l => l.HasOne<Notice>().WithMany()
                        .HasForeignKey("IdNotice")
                        .HasConstraintName("FK_NOTICE_M_LIEN_159_NOTICE"),
                    j =>
                    {
                        j.HasKey("IdNotice", "IdMotCle");
                        j.ToTable("NOTICE_MOT_CLE");
                        j.IndexerProperty<decimal>("IdNotice")
                            .HasColumnType("NUMBER(38)")
                            .HasColumnName("ID_NOTICE");
                        j.IndexerProperty<decimal>("IdMotCle")
                            .HasColumnType("NUMBER(38)")
                            .HasColumnName("ID_MOT_CLE");
                    });

            entity.HasMany(d => d.IdPays).WithMany(p => p.IdNotices)
                .UsingEntity<Dictionary<string, object>>(
                    "PaysPublication",
                    r => r.HasOne<Pay>().WithMany()
                        .HasForeignKey("IdPays")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PAYS_PUB_LIEN_149_PAYS"),
                    l => l.HasOne<Notice>().WithMany()
                        .HasForeignKey("IdNotice")
                        .HasConstraintName("FK_PAYS_PUB_LIEN_148_NOTICE"),
                    j =>
                    {
                        j.HasKey("IdNotice", "IdPays");
                        j.ToTable("PAYS_PUBLICATION");
                        j.IndexerProperty<decimal>("IdNotice")
                            .HasColumnType("NUMBER(38)")
                            .HasColumnName("ID_NOTICE");
                        j.IndexerProperty<string>("IdPays")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .HasColumnName("ID_PAYS");
                    });
        });

        modelBuilder.Entity<NoticeCollection>(entity =>
        {
            entity.HasKey(e => new { e.IdNotice, e.IdCollection });

            entity.ToTable("NOTICE_COLLECTION");

            entity.Property(e => e.IdNotice)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_NOTICE");
            entity.Property(e => e.IdCollection)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_COLLECTION");
            entity.Property(e => e.NumeroDansCollection)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("NUMERO_DANS_COLLECTION");

            entity.HasOne(d => d.IdCollectionNavigation).WithMany(p => p.NoticeCollections)
                .HasForeignKey(d => d.IdCollection)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTICE_C_LIEN_145_COLLECTI");

            entity.HasOne(d => d.IdNoticeNavigation).WithMany(p => p.NoticeCollections)
                .HasForeignKey(d => d.IdNotice)
                .HasConstraintName("FK_NOTICE_C_LIEN_144_NOTICE");
        });

        modelBuilder.Entity<NoticeDipDisEtab>(entity =>
        {
            entity.HasKey(e => e.IdNotice);

            entity.ToTable("NOTICE_DIP_DIS_ETAB");

            entity.Property(e => e.IdNotice)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_NOTICE");
            entity.Property(e => e.AnneSoutenance)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("ANNE_SOUTENANCE");
            entity.Property(e => e.IdDiplome)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_DIPLOME");
            entity.Property(e => e.IdDiscipline)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_DISCIPLINE");
            entity.Property(e => e.IdEtablissement)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_ETABLISSEMENT");
            entity.Property(e => e.NoteTexte)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOTE_TEXTE");

            entity.HasOne(d => d.IdDiplomeNavigation).WithMany(p => p.NoticeDipDisEtabs)
                .HasForeignKey(d => d.IdDiplome)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTICE_D_LIEN_155_DIPLOME");

            entity.HasOne(d => d.IdDisciplineNavigation).WithMany(p => p.NoticeDipDisEtabs)
                .HasForeignKey(d => d.IdDiscipline)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTICE_D_LIEN_157_DISCIPLI");

            entity.HasOne(d => d.IdEtablissementNavigation).WithMany(p => p.NoticeDipDisEtabs)
                .HasForeignKey(d => d.IdEtablissement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTICE_D_LIEN_156_ETABLISS");

            entity.HasOne(d => d.IdNoticeNavigation).WithOne(p => p.NoticeDipDisEtab)
                .HasForeignKey<NoticeDipDisEtab>(d => d.IdNotice)
                .HasConstraintName("FK_NOTICE_D_LIEN_158_NOTICE");
        });

        modelBuilder.Entity<NoticeEdition>(entity =>
        {
            entity.HasKey(e => new { e.IdVille, e.IdEditeur, e.IdNotice });

            entity.ToTable("NOTICE_EDITION");

            entity.Property(e => e.IdVille)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_VILLE");
            entity.Property(e => e.IdEditeur)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_EDITEUR");
            entity.Property(e => e.IdNotice)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_NOTICE");
            entity.Property(e => e.DateEdition)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DATE_EDITION");

            entity.HasOne(d => d.IdEditeurNavigation).WithMany(p => p.NoticeEditions)
                .HasForeignKey(d => d.IdEditeur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTICE_E_LIEN_899_EDITEUR");

            entity.HasOne(d => d.IdNoticeNavigation).WithMany(p => p.NoticeEditions)
                .HasForeignKey(d => d.IdNotice)
                .HasConstraintName("FK_NOTICE_E_LIEN_900_NOTICE");

            entity.HasOne(d => d.IdVilleNavigation).WithMany(p => p.NoticeEditions)
                .HasForeignKey(d => d.IdVille)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTICE_E_LIEN_898_VILLE");
        });

        modelBuilder.Entity<NoticeMentionEdition>(entity =>
        {
            entity.HasKey(e => e.IdNotice).HasName("NOTICE_MENTION_EDITION_PK");

            entity.ToTable("NOTICE_MENTION_EDITION");

            entity.Property(e => e.IdNotice)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_NOTICE");
            entity.Property(e => e.MentionEdition)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("MENTION_EDITION");
        });

        modelBuilder.Entity<NoticeTerme>(entity =>
        {
            entity.HasKey(e => new { e.IdTerme, e.IdNotice });

            entity.ToTable("NOTICE_TERME");

            entity.Property(e => e.IdTerme)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_TERME");
            entity.Property(e => e.IdNotice)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_NOTICE");
            entity.Property(e => e.PoidsTerme)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("POIDS_TERME");

            entity.HasOne(d => d.IdNoticeNavigation).WithMany(p => p.NoticeTermes)
                .HasForeignKey(d => d.IdNotice)
                .HasConstraintName("FK_NOTICE_T_LIEN_508_NOTICE");

            entity.HasOne(d => d.IdTermeNavigation).WithMany(p => p.NoticeTermes)
                .HasForeignKey(d => d.IdTerme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTICE_T_LIEN_507_TERME");
        });

        modelBuilder.Entity<NoticeTermeExact>(entity =>
        {
            entity.HasKey(e => new { e.IdTermeExact, e.IdNotice }).HasName("NOTICE_TERME_EXACT_PK");

            entity.ToTable("NOTICE_TERME_EXACT");

            entity.Property(e => e.IdTermeExact)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_TERME_EXACT");
            entity.Property(e => e.IdNotice)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_NOTICE");
            entity.Property(e => e.PoidsTerme)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("POIDS_TERME");
        });

        modelBuilder.Entity<NoticeTheme>(entity =>
        {
            entity.HasKey(e => new { e.IdNotice, e.IdTheme });

            entity.ToTable("NOTICE_THEME");

            entity.Property(e => e.IdNotice)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_NOTICE");
            entity.Property(e => e.IdTheme)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID_THEME");
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(e => e.IdOperation).HasName("OPERATION_PK");

            entity.ToTable("OPERATION");

            entity.Property(e => e.IdOperation)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_OPERATION");
            entity.Property(e => e.Operation1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OPERATION");
        });

        modelBuilder.Entity<ParametresCatlibPret>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PARAMETRES_CATLIB_PRET");

            entity.Property(e => e.DureeReservation)
                .HasColumnType("NUMBER")
                .HasColumnName("DUREE_RESERVATION");
        });

        modelBuilder.Entity<Pay>(entity =>
        {
            entity.HasKey(e => e.IdPays);

            entity.ToTable("PAYS");

            entity.Property(e => e.IdPays)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID_PAYS");
            entity.Property(e => e.Pays)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PAYS");
        });

        modelBuilder.Entity<Penalite>(entity =>
        {
            entity.HasKey(e => new { e.IdCategorie, e.JoursRetard }).HasName("PENALITE_PK");

            entity.ToTable("PENALITE");

            entity.Property(e => e.IdCategorie)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID_CATEGORIE");
            entity.Property(e => e.JoursRetard)
                .HasColumnType("NUMBER")
                .HasColumnName("JOURS_RETARD");
            entity.Property(e => e.NombreJoursRetard)
                .HasColumnType("NUMBER")
                .HasColumnName("NOMBRE_JOURS_RETARD");
        });

        modelBuilder.Entity<PenaliteAdherent>(entity =>
        {
            entity.HasKey(e => e.IdAdherent).HasName("PENALITE_ADHERENT_PK");

            entity.ToTable("PENALITE_ADHERENT");

            entity.Property(e => e.IdAdherent)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID_ADHERENT");
            entity.Property(e => e.DatePenalite)
                .HasColumnType("DATE")
                .HasColumnName("DATE_PENALITE");
            entity.Property(e => e.NombreJoursPenalite)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMBER")
                .HasColumnName("NOMBRE_JOURS_PENALITE");
        });

        modelBuilder.Entity<PenaliteAdherentTemp>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PENALITE_ADHERENT_TEMP");

            entity.Property(e => e.DatePenalite)
                .HasColumnType("DATE")
                .HasColumnName("DATE_PENALITE");
            entity.Property(e => e.IdAdherent)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID_ADHERENT");
            entity.Property(e => e.NombreJoursPenalite)
                .HasColumnType("NUMBER")
                .HasColumnName("NOMBRE_JOURS_PENALITE");
        });

        modelBuilder.Entity<Periodicite>(entity =>
        {
            entity.HasKey(e => e.IdPeriodicite);

            entity.ToTable("PERIODICITE");

            entity.Property(e => e.IdPeriodicite)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("ID_PERIODICITE");
            entity.Property(e => e.Periodicite1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PERIODICITE");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.IdPosition).HasName("POSITION_PK");

            entity.ToTable("POSITION");

            entity.Property(e => e.IdPosition)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_POSITION");
            entity.Property(e => e.LibellePosition)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LIBELLE_POSITION");
        });

        modelBuilder.Entity<Pret>(entity =>
        {
            entity.HasKey(e => new { e.IdAdherent, e.IdExemplaire, e.DatePret }).HasName("PRET_PK");

            entity.ToTable("PRET");

            entity.Property(e => e.IdAdherent)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID_ADHERENT");
            entity.Property(e => e.IdExemplaire)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID_EXEMPLAIRE");
            entity.Property(e => e.DatePret)
                .HasColumnType("DATE")
                .HasColumnName("DATE_PRET");
            entity.Property(e => e.EtatDuree)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("'F'")
                .HasColumnName("ETAT_DUREE");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => new { e.IdAdherent, e.Cote, e.HeureReservation }).HasName("RESERVATION_PK");

            entity.ToTable("RESERVATION");

            entity.Property(e => e.IdAdherent)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID_ADHERENT");
            entity.Property(e => e.Cote)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("COTE");
            entity.Property(e => e.HeureReservation)
                .HasPrecision(6)
                .HasColumnName("HEURE_RESERVATION");

            entity.HasOne(d => d.IdAdherentNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdAdherent)
                .HasConstraintName("FK_RES");
        });

        modelBuilder.Entity<Selection>(entity =>
        {
            entity.HasKey(e => e.IdSelection).HasName("SELECTION_PK");

            entity.ToTable("SELECTION");

            entity.Property(e => e.IdSelection)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_SELECTION");
            entity.Property(e => e.LibelleSelection)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LIBELLE_SELECTION");

            entity.HasMany(d => d.IdNotices).WithMany(p => p.IdSelections)
                .UsingEntity<Dictionary<string, object>>(
                    "SelectionNotice",
                    r => r.HasOne<Notice>().WithMany()
                        .HasForeignKey("IdNotice")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("SELECTION_NOTICE_NOTICE_FK1"),
                    l => l.HasOne<Selection>().WithMany()
                        .HasForeignKey("IdSelection")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("SELECTION_NOTICE_SELECTIO_FK1"),
                    j =>
                    {
                        j.HasKey("IdSelection", "IdNotice").HasName("SELECTION_NOTICE_PK");
                        j.ToTable("SELECTION_NOTICE");
                        j.IndexerProperty<decimal>("IdSelection")
                            .HasColumnType("NUMBER(38)")
                            .HasColumnName("ID_SELECTION");
                        j.IndexerProperty<decimal>("IdNotice")
                            .HasColumnType("NUMBER(38)")
                            .HasColumnName("ID_NOTICE");
                    });
        });

        modelBuilder.Entity<SourceArticle>(entity =>
        {
            entity.HasKey(e => e.IdSourceArticle);

            entity.ToTable("SOURCE_ARTICLE");

            entity.Property(e => e.IdSourceArticle)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_SOURCE_ARTICLE");
            entity.Property(e => e.DatePubArticle)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("DATE_PUB_ARTICLE");
            entity.Property(e => e.IntervalePage)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("INTERVALE_PAGE");
            entity.Property(e => e.IssnRevue)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ISSN_REVUE");
            entity.Property(e => e.NumeroRevue)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUMERO_REVUE");
            entity.Property(e => e.TitreSourceArticle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITRE_SOURCE_ARTICLE");
        });

        modelBuilder.Entity<TableCdd>(entity =>
        {
            entity.HasKey(e => e.Cdd).HasName("TABLE_CDD_PK");

            entity.ToTable("TABLE_CDD");

            entity.Property(e => e.Cdd)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("CDD");
            entity.Property(e => e.Libelle)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("LIBELLE");
        });

        modelBuilder.Entity<Terme>(entity =>
        {
            entity.HasKey(e => e.IdTerme);

            entity.ToTable("TERME");

            entity.Property(e => e.IdTerme)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_TERME");
            entity.Property(e => e.Terme1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TERME");
        });

        modelBuilder.Entity<TermeExact>(entity =>
        {
            entity.HasKey(e => e.IdTermeExact).HasName("TERME_EXACT_PK");

            entity.ToTable("TERME_EXACT");

            entity.Property(e => e.IdTermeExact)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_TERME_EXACT");
            entity.Property(e => e.TermeExact1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TERME_EXACT");
        });

        modelBuilder.Entity<Theme>(entity =>
        {
            entity.HasKey(e => e.IdTheme);

            entity.ToTable("THEME");

            entity.Property(e => e.IdTheme)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID_THEME");
            entity.Property(e => e.Theme1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("THEME");
        });

        modelBuilder.Entity<TypeNotice>(entity =>
        {
            entity.HasKey(e => e.IdType);

            entity.ToTable("TYPE_NOTICE");

            entity.Property(e => e.IdType)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_TYPE");
            entity.Property(e => e.TypeNotice1)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("TYPE_NOTICE");
        });

        modelBuilder.Entity<Ville>(entity =>
        {
            entity.HasKey(e => e.IdVille);

            entity.ToTable("VILLE");

            entity.Property(e => e.IdVille)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_VILLE");
            entity.Property(e => e.Ville1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VILLE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
