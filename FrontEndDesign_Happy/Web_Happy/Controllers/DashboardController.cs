using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FrontEndDesign_Happy.Models;
using FrontEndDesign_Happy.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Web_Happy.Controllers
{
    public class DashboardController : Controller
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
    }
}
