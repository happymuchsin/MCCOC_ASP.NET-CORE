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
    public class CategorysController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44305/api/")
        };
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadCategory()
        {
            IEnumerable<Category> category = null;
            var responseTask = client.GetAsync("Categorys");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Category>>();
                readTask.Wait();
                category = readTask.Result;
            }
            else
            {
                category = Enumerable.Empty<Category>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(category);
        }

        public JsonResult GetById(int Id)
        {
            Category category = null;
            var responseTask = client.GetAsync("Categorys/" + Id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                category = JsonConvert.DeserializeObject<Category>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(category);
        }

        public JsonResult InsertOrUpdate(Category category)
        {
            var myContent = JsonConvert.SerializeObject(category);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (category.Id == 0)
            {
                var result = client.PostAsync("Categorys", byteContent).Result;
                return Json(result);
            }
            else
            {
                var result = client.PutAsync("Categorys/" + category.Id, byteContent).Result;
                return Json(result);
            }
        }

        public JsonResult Delete(int Id)
        {
            var result = client.DeleteAsync("Categorys/" + Id).Result;
            return Json(result);
        }
    }
}
