using FrontEndDesign_Happy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndDesign_Happy.Repository.Interface
{
    public interface IReceiptRepository
    {
        Task<IEnumerable<ReceiptVM>> GetAll();
        ReceiptVM Get(int Id);
        int Create(ReceiptVM receiptVM);
        int Edit(ReceiptVM receiptVM, int Id);
        int Delete(int Id);
    }
}
