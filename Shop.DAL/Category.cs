using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwę kategorii")]
        [StringLength(100)]
        public string NameCategory { get; set; }
        [Required(ErrorMessage = "Wprowadź opis kategorii")]
        public string DescriptionCategory { get; set; }
        public string NameFileIcon { get; set; }

        public virtual ICollection<Product> Products { get; set; }





    }
}