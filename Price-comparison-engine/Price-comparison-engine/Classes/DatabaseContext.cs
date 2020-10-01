using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Price_comparison_engine.Classes
{
    public class DatabaseContext : DbContext
    {
        public DbSet<UserData> UserData { get; set; }
        public DbSet<ShopRatingData> ShopRatingData { get; set; }
        public DbSet<PageData> PageData { get; set; }
    }
}
