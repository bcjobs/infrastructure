using BookStore.EF6.Configurations;
using BookStore.EF6.Entities;
using BookStore.EF6.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.EF6
{
    class StoreContext : DbContext
    {

        static StoreContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StoreContext, Configuration>());
        }

        public StoreContext() 
            : base("Name=BookStoreEF6")
        {
        }

        public DbSet<EBook> Books { get; set; }
        public DbSet<EAuthor> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new EBookConfiguration());
            modelBuilder.Configurations.Add(new EAuthorConfiguration());
        }
    }
}
