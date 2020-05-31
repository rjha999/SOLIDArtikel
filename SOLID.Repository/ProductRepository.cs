using Microsoft.AspNetCore.Http;
using SOLID.Common.Model;
using SOLID.DAL.DBContext;
using SOLID.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _productContext;
        private readonly IArtikelRepository _artikelRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IColorCodeRepository _colorCodeRepository;
        private readonly IColorRepository _colorRepository;
        public ProductRepository(ProductContext productContext
                                , IArtikelRepository artikelRepository, ICategoryRepository categoryRepository
                                , IColorCodeRepository colorCodeRepository, IColorRepository colorRepository
                                )
        {
            _productContext = productContext;
            _artikelRepository = artikelRepository;
            _categoryRepository = categoryRepository;
            _colorCodeRepository = colorCodeRepository;
            _colorRepository = colorRepository;
        }

        public async Task<bool> AddProducts(IFormFile formFile)
        {
            try
            {
                List<string> categoryNames = new List<string>();
                List<string> colorCodeNames = new List<string>();
                List<string> colorNames = new List<string>();
                List<Product> products = new List<Product>();

                var result = new StringBuilder();

                using (var reader = new StreamReader(formFile.OpenReadStream()))
                {
                    //Initially Skip first Line which cntain header
                    reader.ReadLine();

                    //Read all lines in CSV file, Line by line reading will not even consume more memory 
                    while (reader.Peek() >= 0)
                    {
                        var lineValues = (await reader.ReadLineAsync()).Split(',');
                        ExtractProduct(lineValues, ref colorCodeNames, ref categoryNames, ref colorNames, ref products);
                    }

                }

                //Add All colorCodeNames
                List<ColorCode> colorCodes = colorCodeNames.Distinct().Select(x => new ColorCode { Code = x }).ToList();
                _colorCodeRepository.AddList(colorCodes);

                //Add All categoryNames
                List<Category> categories = categoryNames.Distinct().Select(x => new Category { Name = x }).ToList();
                _categoryRepository.AddList(categories);

                //Add All Colors
                List<Color> colors = colorNames.Distinct().Select(x => new Color { Name = x }).ToList();
                _colorRepository.AddList(colors);

                _productContext.SaveChanges();

                //Add All Artikels
                List<Artikel> artikels = products.Select(x => new Artikel
                {
                    Key = x.Key,
                    ArtikelCode = x.ArtikelCode,
                    ColorCodeId = colorCodes.FirstOrDefault(y=>y.Code==x.ColorCode).Id,
                    Description = x.Description,
                    Price = x.Price,
                    DiscountPrice = x.DiscountPrice,
                    DeliveredIn = x.DeliveredIn,
                    CategoryId = categories.FirstOrDefault(y => y.Name == x.Q1).Id,
                    Size = x.Size,
                    ColorId = colors.FirstOrDefault(y=>y.Name==x.Color).Id

                }).ToList();

                _artikelRepository.AddList(artikels);

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public void ExtractProduct(string[] lineValues, ref List<string> colorCodeNames, ref List<string> categoryNames
                                    , ref List<string> colorNames, ref List<Product> products)
        {
            

            //Read all color Code and Insert
            colorCodeNames.Add(lineValues[2]);

            //Read all Category
            categoryNames.Add(lineValues[7]);

            //Read Colors in each line of CSV
            colorNames.Add(lineValues[9]);

            //Read all Artikels
            products.Add(new Product
            {
                Key = lineValues[0],
                ArtikelCode = lineValues[1],
                ColorCode = lineValues[2],
                Description = lineValues[3],

                //With the assumption that input will be in Decimal format only
                Price = Convert.ToDecimal(lineValues[4]),

                //With the assumption that input will be in Decimal format only
                DiscountPrice = Convert.ToDecimal(lineValues[5]),
                DeliveredIn = lineValues[6],
                Q1 = lineValues[7],

                //With the assumption that input will be in Integer format only
                Size = Convert.ToInt32(lineValues[8]),
                Color = lineValues[9]
            });
        }



    }
}
