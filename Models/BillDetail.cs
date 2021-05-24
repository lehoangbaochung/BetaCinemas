using System.ComponentModel.DataAnnotations.Schema;

namespace BetaCinemas.Models
{
    public partial class BillDetail
    {
        public int Id { get; set; }
        public int BillId { get; set; }

        [ForeignKey("BillId")]
        public virtual Bill Bill { get; set; }
    }
}
