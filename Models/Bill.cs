using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetaCinemas.Models
{
    public partial class Bill
    {
        public Bill()
        {
            BillDetails = new HashSet<BillDetail>();
        }

        public int Id { get; set; }
        public DateTime? SoldTime { get; set; }
        public int Total { get; set; }
        public bool IsSold { get; set; }
        public string About { get; set; }
        public string MemberId { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
