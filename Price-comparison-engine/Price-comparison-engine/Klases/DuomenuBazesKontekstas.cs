using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Price_comparison_engine.Klases
{
    public class DuomenuBazesKontekstas : DbContext
    {
        public DbSet<NaudotojoDuomenys> NaudotojoDuomenys { get; set; }
    }
}
