using Microsoft.AspNetCore.Http;
using SOLID.Common.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOLID.IRepository
{
    public interface IProductRepository 
    {
        Task<bool> AddProducts(IFormFile formFile);
        void ExtractProduct(string[] lineValues, ref List<string> colorCodeNames, ref List<string> categoryNames
            , ref List<string> colorNames, ref List<Product> products);
    }
}
