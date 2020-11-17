using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FrontEndDesign_Happy.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Web_Happy.Controllers
{
    public class ItemsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44305/api/")
        };
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadItem()
        {
            IEnumerable<ItemVM> itemVM;
            var responseTask = client.GetAsync("Items");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<ItemVM>>();
                readTask.Wait();
                itemVM = readTask.Result;
            }
            else
            {
                itemVM = Enumerable.Empty<ItemVM>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(itemVM);
        }

        public JsonResult GetById(int Id)
        {
            ItemVM itemVM = null;
            var responseTask = client.GetAsync("Items/" + Id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                itemVM = JsonConvert.DeserializeObject<ItemVM>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(itemVM);
        }

        public JsonResult InsertOrUpdate(ItemVM itemVM)
        {
            var myContent = JsonConvert.SerializeObject(itemVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (itemVM.Id == 0)
            {
                var result = client.PostAsync("Items", byteContent).Result;
                return Json(result);
            }
            else
            {
                var result = client.PutAsync("Items/" + itemVM.Id, byteContent).Result;
                return Json(result);
            }
        }

        public JsonResult Delete(int Id)
        {
            var result = client.DeleteAsync("Items/" + Id).Result;
            return Json(result);
        }
    }
}
