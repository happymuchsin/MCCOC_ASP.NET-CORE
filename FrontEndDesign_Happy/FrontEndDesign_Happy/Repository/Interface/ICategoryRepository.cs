using FrontEndDesign_Happy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndDesign_Happy.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Category Get(int Id);
        int Create(Category category);
        int Edit(Category category, int Id);
        int Delete(int Id);
    }
}
