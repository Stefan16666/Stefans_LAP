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
    
    public partial class Art
    {
        public Art()
        {
            this.AlleRaeume = new HashSet<Raum>();
        }
    
        public int Id { get; set; }
        public string Bezeichnung { get; set; }
    
        public virtual ICollection<Raum> AlleRaeume { get; set; }
    }
}
