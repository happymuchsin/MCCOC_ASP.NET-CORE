using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndDesign_Happy.ViewModels
{
    public class ReceiptVM
    {
        public int Id { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public int itemId { get; set; }
        public string itemName { get; set; }
        public int Quantity { get; set; }
        public int buyerId { get; set; }
        public string buyerName { get; set; }
        public int TotalPrice { get; set; }
    }
}
