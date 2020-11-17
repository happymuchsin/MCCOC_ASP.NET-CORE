using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEndDesign_Happy.Repository.Interface;
using FrontEndDesign_Happy.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndDesign_Happy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private IItemRepository _itemRepository;
        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemVM>> GetAll()
        {
            return await _itemRepository.GetAll();
        }

        [HttpPost]
        public IActionResult Create(ItemVM itemVM)
        {
            var create = _itemRepository.Create(itemVM);
            if (create > 0)
            {
                return Ok(create);
            }
            return BadRequest("Can't be created");
        }

        [HttpGet("{Id}")]
        public ItemVM GetById(int Id)
        {
            return _itemRepository.Get(Id);
        }

        [HttpPut("{Id}")]
        public IActionResult Edit(ItemVM itemVM, int Id)
        {
            var edit = _itemRepository.Edit(itemVM, Id);

            if (edit > 0)
            {
                return Ok(edit);
            }
            return BadRequest("Can't be edited");
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var delete = _itemRepository.Delete(Id);

            if (delete > 0)
            {
                return Ok(delete);
            }
            return BadRequest("Can't be deleted");
        }
    }
}
