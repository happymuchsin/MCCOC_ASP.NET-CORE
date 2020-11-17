using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FrontEndDesign_Happy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Web_Happy.Controllers
{
    public class ReceiptsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44305/api/")
        };
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadReceipt()
        {
            IEnumerable<ReceiptVM> receiptVM;
            var responseTask = client.GetAsync("Receipts");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<ReceiptVM>>();
                readTask.Wait();
                receiptVM = readTask.Result;
            }
            else
            {
                receiptVM = Enumerable.Empty<ReceiptVM>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(receiptVM);
        }

        public JsonResult GetById(int Id)
        {
            ReceiptVM receiptVM = null;
            var responseTask = client.GetAsync("Receipts/" + Id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                receiptVM = JsonConvert.DeserializeObject<ReceiptVM>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(receiptVM);
        }

        public JsonResult InsertOrUpdate(ReceiptVM receiptVM)
        {
            var myContent = JsonConvert.SerializeObject(receiptVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (receiptVM.Id == 0)
            {
                var result = client.PostAsync("Receipts", byteContent).Result;
                return Json(result);
            }
            else
            {
                var result = client.PutAsync("Receipts/" + receiptVM.Id, byteContent).Result;
                return Json(result);
            }
        }

        public JsonResult Delete(int Id)
        {
            var result = client.DeleteAsync("Receipts/" + Id).Result;
            return Json(result);
        }
    }
}
