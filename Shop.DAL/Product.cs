using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Product
    {
        
            public int ProductId { get; set; }
            public int CategoryId { get; set; }
            [Required(ErrorMessage = "Wprowadź nazwę kursu")]
            [StringLength(100)]
            public string Title { get; set; }
            [Required(ErrorMessage = "Wprowadź nazwę autora")]
            [StringLength(100)]
            public string Company { get; set; }
            public DateTime DateAdded { get; set; }
            [StringLength(100)]
            public string NameFileImage { get; set; }
            public string Description { get; set; }
            public decimal PriceProduct { get; set; }
            public bool Bestseller { get; set; }
            public bool Hidden { get; set; }
            public string ShortDescription { get; set; }

            public virtual Category Category { get; set; }
        }
    

}