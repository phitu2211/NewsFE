using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsFE.Models;

namespace NewsFE.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;


        public HomeController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("myApi");
        }

        public IActionResult Index()
        {
            var response = _httpClient.GetAsync("/api/v1/news?filter={}").Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var data = JsonSerializer.Deserialize<Response<List<Post>>>(jsonData);
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
