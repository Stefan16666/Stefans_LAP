
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
    using System.Collections.Generic;
    
public partial class Firma
{

    public Firma()
    {

        this.AlleBenutzer = new HashSet<Benutzer>();

        this.AlleBuchnungen = new HashSet<Buchung>();

    }


    public int Id { get; set; }

    public string Strasse { get; set; }

    public string Nummer { get; set; }

    public string Plz { get; set; }

    public string Ort { get; set; }

    public string Bezeichnung { get; set; }

    public bool aktiv { get; set; }



    public virtual ICollection<Benutzer> AlleBenutzer { get; set; }

    public virtual ICollection<Buchung> AlleBuchnungen { get; set; }

}

}
