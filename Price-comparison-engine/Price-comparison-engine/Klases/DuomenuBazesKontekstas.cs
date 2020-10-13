using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Price_comparison_engine.Klases
{
    public class DuomenuBazesKontekstas : DbContext
    {
        public DuomenuBazesKontekstas():base("name=DuomenuBazesKontekstas")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<NaudotojoDuomenys> NaudotojoDuomenys { get; set; }
        public DbSet<ShopRatingTable> ShopRatingTable { get; set; }
        public DbSet<ItemsTable> ItemsTable { get; set; }
        public DbSet<SavedItems> SavedItems { get; set; }
        public DbSet<CommentsTable> CommentsTable { get; set; }
    }
}
