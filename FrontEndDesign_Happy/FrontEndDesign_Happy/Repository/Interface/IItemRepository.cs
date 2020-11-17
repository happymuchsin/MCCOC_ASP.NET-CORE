using FrontEndDesign_Happy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndDesign_Happy.Repository.Interface
{
    public interface IItemRepository
    {
        Task<IEnumerable<ItemVM>> GetAll();
        ItemVM Get(int Id);
        int Create(ItemVM itemVM);
        int Edit(ItemVM itemVM, int Id);
        int Delete(int Id);
    }
}
