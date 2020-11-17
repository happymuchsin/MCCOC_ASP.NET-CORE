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
    public class ReceiptsController : ControllerBase
    {
        private IReceiptRepository _receiptRepository;
        public ReceiptsController(IReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ReceiptVM>> GetAll()
        {
            return await _receiptRepository.GetAll();
        }

        [HttpPost]
        public IActionResult Create(ReceiptVM receiptVM)
        {
            var create = _receiptRepository.Create(receiptVM);
            if (create > 0)
            {
                return Ok(create);
            }
            return BadRequest("Can't be created");
        }

        [HttpGet("{Id}")]
        public ReceiptVM GetById(int Id)
        {
            return _receiptRepository.Get(Id);
        }

        [HttpPut("{Id}")]
        public IActionResult Edit(ReceiptVM receiptVM, int Id)
        {
            var edit = _receiptRepository.Edit(receiptVM, Id);

            if (edit > 0)
            {
                return Ok(edit);
            }
            return BadRequest("Can't be edited");
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var delete = _receiptRepository.Delete(Id);

            if (delete > 0)
            {
                return Ok(delete);
            }
            return BadRequest("Can't be deleted");
        }
    }
}
