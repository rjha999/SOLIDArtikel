using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SOLID.Common.Model;
using SOLID.MVC.Models;

namespace SOLID.MVC.Controllers
{

    //[RequestSizeLimit(long.MaxValue)]
    [DisableRequestSizeLimit]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //[RequestSizeLimit(long.MaxValue)]
        public async Task<IActionResult> UploadFile(IFormFile postedFile)
        {
            try
            {
                if (postedFile != null && postedFile.Length > 0)
                {
                    using (var client = new HttpClient())
                    {
                        
                            string url = "http://localhost:59285/api/product";
                            client.BaseAddress = new Uri(url);

                            byte[] data;
                            using (var br = new BinaryReader(postedFile.OpenReadStream()))
                                data = br.ReadBytes((int)postedFile.OpenReadStream().Length);

                            ByteArrayContent bytes = new ByteArrayContent(data);

                            MultipartFormDataContent multiContent = new MultipartFormDataContent();

                            multiContent.Add(bytes, "file", postedFile.FileName);


                            var result = client.PostAsync(url, multiContent).Result;

                            return RedirectToAction("Artikel");
                        
                    }
                }

                return StatusCode(400); // 400 is bad request

            }
            catch (Exception)
            {
                return StatusCode(500); // 500 is generic server error
            }
        }

        [HttpGet]
        public IActionResult Artikel()
        {
            using (var client = new HttpClient())
            {

                string url = "http://localhost:59285/api/product/";
                client.BaseAddress = new Uri(url);

                var result =client.GetAsync(url).Result;


                List<Artikel> artikels = new List<Artikel>();
                var jsonString = result.Content.ReadAsStringAsync();
                jsonString.Wait();
                artikels = JsonConvert.DeserializeObject<List<Artikel>>(jsonString.Result);


                return View(artikels);
            }
        }
    }
}
