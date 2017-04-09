using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shop.Migrations;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Shop.DAL
{
    public class ProductsInitializer: MigrateDatabaseToLatestVersion<ProductsContext, Configuration>
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


        public static void SeedUzytkownicy(ProductsContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            const string name = "wp@wp.pl";
            const string password = "Luki91!";
            const string roleName = "Admin";

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, DataUser = new DataUser() };
                var result = userManager.Create(user, password);
            }

            // utworzenie roli Admin jeśli nie istnieje 
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            // dodanie uzytkownika do roli Admin jesli juz nie jest w roli
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}