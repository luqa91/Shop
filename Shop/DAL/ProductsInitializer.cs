using Shop.Migrations;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Shop.DAL
{
    public class ProductsInitializer: MigrateDatabaseToLatestVersion<ProductsContext, Configuration >
    {

        public static void SeedProductsData(ProductsContext context)
        {
            var category = new List<Category>
            {
                new Category() { CategoryId=1, NameCategory="Electronics", NameFileIcon="Electronics.png", DescriptionCategory="Opis electronics" },
                new Category() { CategoryId=2, NameCategory="Technologies", NameFileIcon="Technologies.png", DescriptionCategory="Opis technologies" },
                new Category() { CategoryId=3, NameCategory="Home", NameFileIcon="Home.png", DescriptionCategory="Opis home" }
            };
            category.ForEach(k=> context.Categories.AddOrUpdate(k));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product() {ProductId=1, Company="Apple",Title="IPhone", CategoryId=1,PriceProduct=2999,Bestseller=true,NameFileImage="IPhone2.png",DateAdded=DateTime.Now, Description="description iphone product" },
                new Product() {ProductId=2, Company="Apple2",Title="IPhone2", CategoryId=1,PriceProduct=3211,Bestseller=true,NameFileImage="IPhone7.png",DateAdded=DateTime.Now, Description="description iphone product" }

            };
            products.ForEach(k => context.Products.AddOrUpdate(k));
            context.SaveChanges();



        }
    }
}