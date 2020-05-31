using SOLID.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.IRepository
{
    public interface IArtikelRepository : IRepo<Artikel>
    {
        IEnumerable<Artikel> GetAllArtikel(int maxRowLimit);
    }
}
