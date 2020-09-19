using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Price_Comparison_Engine.Domain.Modeliai;

namespace Price_comparison_engine.EntityFramework
{
    public class PriceComparisonEngineDbContext : DbContext
    {
        public DbSet<Naudotojas> Naudotojai { get; set; }
        public DbSet<Paskyra> Paskyros { get; set; }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=DuomenuBaze;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
