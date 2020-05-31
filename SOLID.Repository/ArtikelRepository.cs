using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using SOLID.Common.Model;
using SOLID.DAL.DBContext;
using SOLID.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Repository
{
    //Interface Segregation Principle with IRepo for some basic functionnality
    public class ArtikelRepository : IArtikelRepository 
    {
        private ProductContext _productContext;

        public ArtikelRepository(ProductContext productContext)
        {
            this._productContext = productContext;
        }
        public Artikel Add(Artikel data)
        {
            throw new NotImplementedException();
        }

        public bool AddList(List<Artikel> artikels)
        {
            _productContext.BulkInsert(artikels);
            
            return true;
        }

        public Artikel Get(string name)
        {
            return _productContext.Artikels.FirstOrDefault(x=>x.Key==name);
        }

        public  IEnumerable<Artikel> GetAllArtikel(int maxRowLimit=0)
        {
            List<Artikel> artikels= _productContext.Artikels
                                                .Include(x => x.ColorCode)
                                                .Include(x => x.Category)
                                                .Include(x => x.Color)
                                                .Take(maxRowLimit)
                                                .ToList();
            return artikels;
        }
    }
}
