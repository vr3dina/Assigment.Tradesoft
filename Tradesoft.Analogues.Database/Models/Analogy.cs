using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradesoft.Analogues.Database.Models
{
    public class Analogy
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product1Id { get; set; }

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product2Id { get; set; }

        [ForeignKey("Product1Id")]
        public virtual Product Product1  { get; set; }

        [ForeignKey("Product2Id")]
        public virtual Product Product2 { get; set; }

        public int TrustLevel { get; set; }
    }
}
