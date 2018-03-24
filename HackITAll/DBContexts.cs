using HackITAll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HackITAll
{
    public class DBContexts : DbContext
    {

        static DBContexts()
        {
            Database.SetInitializer<DBContexts>(null);
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Contact> Contact { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Product>()
               .HasRequired<Users>(s => s.User)
               .WithMany(g => g.Products)
               .HasForeignKey<int>(s => s.UserId);

        }


    }
}