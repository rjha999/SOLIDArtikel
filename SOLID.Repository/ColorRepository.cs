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
    public class ColorRepository : IColorRepository
    {
        private ProductContext _productContext;

        public ColorRepository(ProductContext productContext)
        {
            this._productContext = productContext;
        }

        public Color Add(Color data)
        {
            throw new NotImplementedException();
        }

        public bool AddList(List<Color> colors)
        {
            _productContext.Colors.AddRangeAsync(colors);
            _productContext.SaveChanges();

            return true;
        }


        public Color Get(string name)
        {
            return _productContext.Colors.FirstOrDefault(x => x.Name == name);
        }
    }
}
