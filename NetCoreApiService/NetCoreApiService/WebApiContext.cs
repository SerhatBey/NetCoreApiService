using Microsoft.EntityFrameworkCore;
using NetCoreApiService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApiService
{
    public class WebApiContext:DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=farmazonDB;Trusted_Connection=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                // Set key for entity
                entity.HasKey(p => p.User_ID);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                // Set key for entity
                entity.HasKey(p => p.Product_ID);
            });

           


            base.OnModelCreating(modelBuilder);
        }

        internal Task FindAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
