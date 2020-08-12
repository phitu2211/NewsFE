using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsFE.Helper;
using NewsFE.Models;

namespace NewsFE.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;


        public CategoryController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("myApi");
        }

        [AcceptVerbs("GET", "HEAD", Route = "/category/{parameter}")]
        public async Task<IActionResult> IndexAsync(string parameter, int? pageNumber)
        {
            String id = "";
            switch (parameter)
            {
                case "bong-da":
                    id = "dd480abd-2d05-466d-b2c2-191150faca9c";
                    break;
                case "my":
                    id = "14000ba7-d072-4577-b915-292c3d72409f";
                    break;
                case "viet-nam":
                    id = "e26e25dc-005c-4184-8172-9c1fd16ef55d";
                    break;
                case "the-gioi":
                    id = "52406325-0759-4f90-a3e0-573677486e03";
                    break;
                case "the-thao":
                    id = "c583b740-7d93-46bb-bfdd-ab2cacce2267";
                    break;
                default:
                    break;
            }
            var response = _httpClient.GetAsync("/api/v1/news/category/"+id).Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var data = JsonSerializer.Deserialize<Response<PostOfCategory>>(jsonData);

            var model = await PaginatedList<Post>.CreateAsync(data.data.newsBelongCategories, pageNumber ?? 1, 7);

            return View(model);
        }
    }
}