using Microsoft.AspNet.Identity.EntityFramework;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Shop.DAL
{
    public class ProductsContext: IdentityDbContext<ApplicationUser>
    {
        public ProductsContext() : base("ProductsContext", throwIfV1Schema: false)
        {

        }


        static ProductsContext()
        {
            Database.SetInitializer<ProductsContext>(new ProductsInitializer());

        }
        public static ProductsContext Create()
        {
            return new ProductsContext();
        }




        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PositionOrder> PositionsOrder { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //using System.data.entity.model configuration.conventions;
            //wyłącza konwencję, która automatycznie tworzy liczbę mnogą dla nazw tabel w bazie danych
            //zamiast Kategorie zostałyby stworzone tabele o nazwie Kategories
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

        }
        
    }
}