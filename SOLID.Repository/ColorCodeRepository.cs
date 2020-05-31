using EFCore.BulkExtensions;
using SOLID.Common.Model;
using SOLID.DAL.DBContext;
using SOLID.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.Repository
{
    //Interface Segregation Principle with IRepo for some basic functionnality
    public class ColorCodeRepository : IColorCodeRepository
    {
        private ProductContext _productContext;

        public ColorCodeRepository(ProductContext productContext)
        {
            this._productContext = productContext;
        }
        public ColorCode Add(ColorCode data)
        {
            throw new NotImplementedException();
        }

        public bool AddList(List<ColorCode> colorCodes)
        {
            _productContext.ColorCodes.AddRangeAsync(colorCodes);
            _productContext.SaveChanges();

            return true;
        }

        public ColorCode Get(string name)
        {
            return _productContext.ColorCodes.FirstOrDefault(x => x.Code == name);
        }
    }
}
