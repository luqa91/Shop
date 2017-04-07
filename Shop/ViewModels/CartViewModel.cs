using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class CartViewModel
    {
        public List<PositionCart> PositionCart { get; set; }
        public decimal PriceTotal { get; set; }

    }
}