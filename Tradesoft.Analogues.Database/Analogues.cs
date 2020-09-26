namespace Tradesoft.Analogues.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Analogues
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnalogyId1 { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnalogyId2 { get; set; }

        public int TrustLevel { get; set; }

        public virtual Products Products { get; set; }

        public virtual Products Products1 { get; set; }
    }
}
