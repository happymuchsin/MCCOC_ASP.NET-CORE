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
    public class CheckoutsController : ControllerBase
    {
        private ICheckoutRepository _checkoutRepository;
        public CheckoutsController(ICheckoutRepository checkoutRepository)
        {
            _checkoutRepository = checkoutRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CheckoutVM>> GetAll()
        {
            return await _checkoutRepository.GetAll();
        }

        [HttpPost]
        public IActionResult Create(CheckoutVM checkoutVM)
        {
            var create = _checkoutRepository.Create(checkoutVM);
            if (create > 0)
            {
                return Ok(create);
            }
            return BadRequest("Can't be created");
        }

        [HttpGet("{Id}")]
        public CheckoutVM GetById(int Id)
        {
            return _checkoutRepository.Get(Id);
        }

        [HttpPut("{Id}")]
        public IActionResult Edit(CheckoutVM checkoutVM, int Id)
        {
            var edit = _checkoutRepository.Edit(checkoutVM, Id);

            if (edit > 0)
            {
                return Ok(edit);
            }
            return BadRequest("Can't be edited");
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var delete = _checkoutRepository.Delete(Id);

            if (delete > 0)
            {
                return Ok(delete);
            }
            return BadRequest("Can't be deleted");
        }
    }
}
