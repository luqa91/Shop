using Postal;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class ConfirmationOrderEmail: Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public decimal Value { get; set; }
        public int NumberOrder { get; set; }
        public string PathImage { get; set; }
        public List<PositionOrder> PositionOrder { get; set; }
    }

    public class OrderRealizedEmail : Email
    {

        public string To { get; set; }
        public string From { get; set; }
        public int NumberOrder { get; set; }

    }



    

}