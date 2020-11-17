using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndDesign_Happy.Models
{
    [Table("TB_M_Checkout")]
    public class Checkout
    {
        [Key]
        public int Id { get; set; }
        public Receipt receipt { get; set; }
    }
}
