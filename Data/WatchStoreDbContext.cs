using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WatchStore.Data.ProductServe;

namespace WatchStore.Data
{
    public class WatchStoreDbContext : IdentityDbContext
    {
        public WatchStoreDbContext(DbContextOptions<WatchStoreDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(seedProductData());
            base.OnModelCreating(modelBuilder);
        }

        private List<Product> seedProductData()
        {
            return new List<Product>
            {
                new Product { Id = 01, Name = "Hublot Gold & Black", Price = 120, Quantity = 10, Description ="This is a best gaming laptop", Image= "/images/hublot1.jpeg" },
                new Product { Id = 02, Name = "Hublot Gold & Red", Price = 130, Quantity = 50, Description ="This is a Office Application", Image= "/images/hublot2.jpeg"},
                new Product { Id = 03, Name = "Hublot All Red", Price = 200, Quantity = 20, Description ="The mouse that works on all surface", Image= "/images/hublot3.jpeg"},
                new Product { Id = 04, Name = "Hublot Silver & Black", Price = 170, Quantity = 20, Description ="The mouse that works on all surface", Image= "/images/hublot4.jpeg"},
                new Product { Id = 05, Name = "Hublot All Black", Price = 190, Quantity = 20, Description ="The mouse that works on all surface", Image= "/images/hublot5.jpeg"},
                new Product { Id = 06, Name = "Hublot All Gold", Price = 200, Quantity = 20, Description ="To store 256GB of data", Image= "/images/hublot6.jpeg"},
                new Product { Id = 07, Name = "Hublot Gold & Black", Price = 120, Quantity = 10, Description ="This is a best gaming laptop", Image= "/images/hublot1.jpeg" },
                new Product { Id = 08, Name = "Hublot Gold & Red", Price = 130, Quantity = 50, Description ="This is a Office Application", Image= "/images/hublot2.jpeg"},
                new Product { Id = 09, Name = "Hublot All Red", Price = 200, Quantity = 20, Description ="The mouse that works on all surface", Image= "/images/hublot3.jpeg"},
                new Product { Id = 10, Name = "Hublot All Gold", Price = 200, Quantity = 20, Description ="To store 256GB of data", Image= "/images/hublot6.jpeg"},
                new Product { Id = 11, Name = "Hublot All Black", Price = 190, Quantity = 20, Description ="The mouse that works on all surface", Image= "/images/hublot5.jpeg"}
            };
        }
    }
}
