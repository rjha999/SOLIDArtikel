using Microsoft.EntityFrameworkCore;
using SOLID.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID.DAL.DBContext
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options): base(options)
        { }
        public virtual DbSet<Artikel> Artikels { get; set; }
        public virtual DbSet<ColorCode> ColorCodes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Color> Colors { get; set; }


        //Database Configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-RVO7APV\SQLEXPRESS; Initial Catalog=StoreNew; Integrated Security=true;");
        }
    }
}
