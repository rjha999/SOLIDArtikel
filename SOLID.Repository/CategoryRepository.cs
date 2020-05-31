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
    public class CategoryRepository : ICategoryRepository
    {
        private ProductContext _productContext;

        public CategoryRepository(ProductContext productContext)
        {
            this._productContext = productContext;
        }
        public Category Add(Category data)
        {
            throw new NotImplementedException();
        }

        public bool AddList(List<Category> categories)
        {
            _productContext.Categories.AddRangeAsync(categories);
            _productContext.SaveChanges();

            return true;
        }

        public Category Get(string name)
        {
            return _productContext.Categories.FirstOrDefault(x => x.Name == name);
        }
    }
}
