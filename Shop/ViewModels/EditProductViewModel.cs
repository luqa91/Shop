using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class EditProductViewModel
    {

        public Product Product { get; set; }
        public IEnumerable<Category> Category { get; set; }

        public bool? Confirmation { get; set; }









    }
}