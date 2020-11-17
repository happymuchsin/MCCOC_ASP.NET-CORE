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
    public class BuyersController : ControllerBase
    {
        private IBuyerRepository _buyerRepository;
        public BuyersController(IBuyerRepository buyerRepository)
        {
            _buyerRepository = buyerRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Buyer>> GetAll()
        {
            return await _buyerRepository.GetAll();
        }

        [HttpPost]
        public IActionResult Create(Buyer buyer)
        {
            var create = _buyerRepository.Create(buyer);
            if (create > 0)
            {
                return Ok(create);
            }
            return BadRequest("Can't be created");
        }

        [HttpGet("{Id}")]
        public Buyer GetById(int Id)
        {
            return _buyerRepository.Get(Id);
        }

        [HttpPut("{Id}")]
        public IActionResult Edit(Buyer buyer, int Id)
        {
            var edit = _buyerRepository.Edit(buyer, Id);

            if (edit > 0)
            {
                return Ok(edit);
            }
            return BadRequest("Can't be edited");
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var delete = _buyerRepository.Delete(Id);

            if (delete > 0)
            {
                return Ok(delete);
            }
            return BadRequest("Can't be deleted");
        }
    }
}
