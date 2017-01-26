namespace DbVerbindung
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbTest : DbContext
    {
        public DbTest()
            : base("name=DbTest")
        {
        }

        public virtual DbSet<Bild> Bild { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
