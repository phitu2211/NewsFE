using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsFE.Models;

namespace NewsFE.Controllers
{
    public class PostController : Controller
    {
        private readonly HttpClient _httpClient;


        public PostController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("myApi");
        }

        [AcceptVerbs("GET", "HEAD", Route = "/post/{parameter}")]
        public IActionResult Index(string parameter)
        {
            String id = parameter.Split('=')[1];
            var response = _httpClient.GetAsync("/api/v1/news/"+ id).Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var data = JsonSerializer.Deserialize<Response<Post>>(jsonData);
        
            response = _httpClient.GetAsync("/api/v1/news?filter={}").Result;
            jsonData = response.Content.ReadAsStringAsync().Result;
            var relatedPost = JsonSerializer.Deserialize<Response<List<Post>>>(jsonData);

            return View(new PostDetails {post = data.data, relatedPost = relatedPost.data});
        }
    }
}