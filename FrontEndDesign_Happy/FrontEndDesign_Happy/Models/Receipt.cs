using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndDesign_Happy.Models
{
    [Table("TB_M_Receipt")]
    public class Receipt
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Item item { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public Buyer buyer { get; set; }
    }
}
