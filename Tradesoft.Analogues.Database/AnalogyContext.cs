using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Tradesoft.Analogues.Database.Models;

namespace Tradesoft.Analogues.Database
{

    public partial class AnalogyContext : DbContext
    {
        public AnalogyContext()
            : base("DBConnection")
        {
        }

        public virtual DbSet<Analogy> Analogues { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
