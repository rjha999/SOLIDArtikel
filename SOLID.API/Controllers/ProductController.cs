using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOLID.Common.Model;
using SOLID.DAL.DBContext;
using SOLID.IRepository;
using SOLID.Repository;

namespace SOLID.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequestSizeLimit(long.MaxValue)]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _productContext;
        private readonly IProductRepository _productRepository;
        private readonly IArtikelRepository _artikelRepository;

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ProductController> _logger;

        //Dependency Inversion Principle
        public ProductController(ProductContext productContext, IProductRepository productRepository
                                ,IArtikelRepository artikelRepository,ILogger<ProductController> logger
                                , IWebHostEnvironment webHostEnvironment)
        {
            _productContext = productContext;
            _productRepository = productRepository;
            _artikelRepository = artikelRepository;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        // GET: api/Product
        //GET all Product/Artikel
        [HttpGet]
        public   IEnumerable<Artikel> Get()
        {
            return _artikelRepository.GetAllArtikel(100);
            //return Ok();
            
        }




        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult>  Post()
        {
            try
            {
                //If there is any file uploaded by user then Only Process
                if (Request.HasFormContentType)
                {
                    var form = Request.Form;
                    foreach (var formFile in form.Files)
                    {
                        //Add Product/Artikel from uploaded CSV file
                        bool result=await _productRepository.AddProducts(formFile);

                        //Create JSON file
                        await CreateJSONFile();
                    }
                }
                return StatusCode(201);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while adding Artikel: {ex.Message}");
                return StatusCode(500);
            }

            
        }

        //Create JSON File of Artikel in 'JSON' folder
        private async Task CreateJSONFile()
        {
            IEnumerable<Artikel> artikels = _artikelRepository.GetAllArtikel(int.MaxValue);
            string json = JsonConvert.SerializeObject(artikels.ToArray());
            var fileName = Guid.NewGuid().ToString() + ".json";
            string filePath = Path.Combine("JSON", fileName);
            System.IO.File.WriteAllText(filePath, json);

        }

        

    }
}
