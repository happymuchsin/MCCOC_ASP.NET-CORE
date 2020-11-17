using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FrontEndDesign_Happy.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Web_Happy.Controllers
{
    public class BuyersController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44305/api/")
        };
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadBuyer()
        {
            IEnumerable<Buyer> buyer = null;
            var responseTask = client.GetAsync("Buyers");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Buyer>>();
                readTask.Wait();
                buyer = readTask.Result;
            }
            else
            {
                buyer = Enumerable.Empty<Buyer>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(buyer);
        }

        public JsonResult GetById(int Id)
        {
            Buyer buyer = null;
            var responseTask = client.GetAsync("Buyers/" + Id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                buyer = JsonConvert.DeserializeObject<Buyer>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(buyer);
        }

        public JsonResult InsertOrUpdate(Buyer buyer)
        {
            var myContent = JsonConvert.SerializeObject(buyer);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (buyer.Id == 0)
            {
                var result = client.PostAsync("Buyers", byteContent).Result;
                return Json(result);
            }
            else
            {
                var result = client.PutAsync("Buyers/" + buyer.Id, byteContent).Result;
                return Json(result);
            }
        }

        public JsonResult Delete(int Id)
        {
            var result = client.DeleteAsync("Buyers/" + Id).Result;
            return Json(result);
        }
    }
}
