﻿

//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Innovation4Austria.logic
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class Innovation4AustriaEntities : DbContext
{
    public Innovation4AustriaEntities()
        : base("name=Innovation4AustriaEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public DbSet<Art> AlleArten { get; set; }

    public DbSet<Ausstattung> AlleAusstattungen { get; set; }

    public DbSet<Bauwerk> AlleBauwerke { get; set; }

    public DbSet<Benutzer> AlleBenutzer { get; set; }

    public DbSet<Bild> AlleBilder { get; set; }

    public DbSet<Buchung> AlleBuchungen { get; set; }

    public DbSet<Buchungsdetails> AlleBuchungsdetails { get; set; }

    public DbSet<Firma> AlleFirmen { get; set; }

    public DbSet<Log> AlleLog { get; set; }

    public DbSet<Raum> AlleRaeume { get; set; }

    public DbSet<Raum_Ausstattung> AlleRaum_Ausstattungen { get; set; }

    public DbSet<Rechnung> AlleRechnungen { get; set; }

    public DbSet<Rechnungsdetails> AlleRechnungsdetails { get; set; }

    public DbSet<Rolle> AlleRollen { get; set; }

    public DbSet<AlleRechnungenEinerFirma> AlleRechnungenEinerFirma { get; set; }

}

}

