﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Price_comparison_engine.Classes
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext():base("name=DatabaseContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<UserData> UserData { get; set; }
        public DbSet<ShopRatingTable> ShopRatingTable { get; set; }
        public DbSet<ItemsTable> ItemsTable { get; set; }
        public DbSet<SavedItems> SavedItems { get; set; }
        public DbSet<CommentsTable> CommentsTable { get; set; }
    }
}
