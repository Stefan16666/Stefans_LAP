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
    
    public partial class Raum_Ausstattung
    {
        public int Id { get; set; }
        public int Ausstattungs_Id { get; set; }
        public Nullable<int> Raum_id { get; set; }
        public int Anzahl_Ausstattungen { get; set; }
        public Nullable<bool> Aktiv { get; set; }
    
        public virtual Ausstattung Ausstattung { get; set; }
        public virtual Raum Raum { get; set; }
    }
}
