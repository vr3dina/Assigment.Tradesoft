namespace Tradesoft.Analogues.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Products()
        //{
        //    Analogues = new HashSet<Analogues>();
        //    Analogues1 = new HashSet<Analogues>();
        //}

        public int Id { get; set; }

        [Required]
        public string VendorCode { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Analogues> Analogues { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Analogues> Analogues1 { get; set; }
    }
}
