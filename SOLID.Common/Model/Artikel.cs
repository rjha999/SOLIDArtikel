using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID.Common.Model
{
    public class Artikel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string ArtikelCode { get; set; }
        public int ColorCodeId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string DeliveredIn { get; set; }
        public int CategoryId { get; set; }
        public int Size { get; set; }
        public int ColorId { get; set; }
        public ColorCode ColorCode { get; set; }
        public Category Category { get; set; }
        public Color Color { get; set; }

    }
}
