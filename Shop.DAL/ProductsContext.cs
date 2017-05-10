
using Shop.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Shop.DAL
{
    public class ProductsContext: DbContext
    {
        public ProductsContext() : base("Data Source=DESKTOP-QCSLQ6L;Initial Catalog=PraktyczneKursy;Integrated Security=True")
        {

        }






        public DbSet<Product> Products { get; set; }


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