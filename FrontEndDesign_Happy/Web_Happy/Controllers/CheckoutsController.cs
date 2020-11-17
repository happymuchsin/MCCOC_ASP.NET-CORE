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
    public class CheckoutsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44305/api/")
        };
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadCheckout()
        {
            IEnumerable<CheckoutVM> checkoutVM;
            var responseTask = client.GetAsync("Checkouts");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<CheckoutVM>>();
                readTask.Wait();
                checkoutVM = readTask.Result;
            }
            else
            {
                checkoutVM = Enumerable.Empty<CheckoutVM>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(checkoutVM);
        }

        public JsonResult GetById(int Id)
        {
            CheckoutVM checkoutVM = null;
            var responseTask = client.GetAsync("Checkouts/" + Id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                checkoutVM = JsonConvert.DeserializeObject<CheckoutVM>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(checkoutVM);
        }

        public JsonResult InsertOrUpdate(CheckoutVM checkoutVM)
        {
            var myContent = JsonConvert.SerializeObject(checkoutVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (checkoutVM.Id == 0)
            {
                var result = client.PostAsync("Checkouts", byteContent).Result;
                return Json(result);
            }
            else
            {
                var result = client.PutAsync("Checkouts/" + checkoutVM.Id, byteContent).Result;
                return Json(result);
            }
        }

        public JsonResult Delete(int Id)
        {
            var result = client.DeleteAsync("Checkouts/" + Id).Result;
            return Json(result);
        }
    }
}
