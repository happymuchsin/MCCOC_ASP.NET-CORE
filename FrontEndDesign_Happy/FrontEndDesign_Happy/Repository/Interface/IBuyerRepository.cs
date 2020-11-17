using FrontEndDesign_Happy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndDesign_Happy.Repository.Interface
{
    public interface IBuyerRepository
    {
        Task<IEnumerable<Buyer>> GetAll();
        Buyer Get(int Id);
        int Create(Buyer buyer);
        int Edit(Buyer buyer, int Id);
        int Delete(int Id);
    }
}
