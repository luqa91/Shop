using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class CartDeleteViewModel
    {
        public decimal CartPriceTotal { get; set; }
        public int CartQuantityPosition { get; set; }

        public int QuantityPositionDeleting { get; set; }
        public int IdPositionDeleting { get; set; }




    }
}