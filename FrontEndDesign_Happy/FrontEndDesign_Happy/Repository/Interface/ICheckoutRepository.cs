using FrontEndDesign_Happy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndDesign_Happy.Repository.Interface
{
    public interface ICheckoutRepository
    {
        Task<IEnumerable<CheckoutVM>> GetAll();
        CheckoutVM Get(int Id);
        int Create(CheckoutVM checkoutVM);
        int Edit(CheckoutVM checkoutVM, int Id);
        int Delete(int Id);
    }
}
