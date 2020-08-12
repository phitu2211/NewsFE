using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using NewsFE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewsFE.ViewComponents
{
    public class RightContentComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        
        public RightContentComponent(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("myApi");
        }


        public IViewComponentResult Invoke()
        {
            var response = _httpClient.GetAsync("/api/v1/news?filter={}").Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var data = JsonSerializer.Deserialize<Response<List<Post>>>(jsonData);
            return View(data);
        }
    }
}
