namespace DbVerbindung
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bild")]
    public partial class Bild
    {
        public int id { get; set; }

        [Required]
        public byte[] bilddaten { get; set; }

        public int? raum_id { get; set; }
    }
}
