using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndDesign_Happy.ViewModels
{
    public class CheckoutVM
    {
        public int Id { get; set; }
        public int receiptId { get; set; }
        public int receiptTotalPrice { get; set; }
    }
}
