using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEndDesign_Happy.Models;
using FrontEndDesign_Happy.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndDesign_Happy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private ICategoryRepository _categoryRepository;
        public CategorysController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            var create = _categoryRepository.Create(category);
            if (create > 0)
            {
                return Ok(create);
            }
            return BadRequest("Can't be created");
        }

        [HttpGet("{Id}")]
        public Category Get(int Id)
        {
            return _categoryRepository.Get(Id);
        }

        [HttpPut("{Id}")]
        public IActionResult Edit(Category category, int Id)
        {
            var edit = _categoryRepository.Edit(category, Id);

            if (edit > 0)
            {
                return Ok(edit);
            }
            return BadRequest("Can't be edited");
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var delete = _categoryRepository.Delete(Id);

            if (delete > 0)
            {
                return Ok(delete);
            }
            return BadRequest("Can't be deleted");
        }
    }
}
