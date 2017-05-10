using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Infrastructure
{
    public class PostalMailService: IMailService
    {
        public void SendingConfirmationOrdersEmail(Order order)
        {
            ConfirmationOrderEmail email = new ConfirmationOrderEmail();
            email.To = order.Email;
            email.From = "Kaczmareek.lukasz@gmail.com";
            email.Value = order.ValueOrder;
            email.NumberOrder = order.OrderId;
            email.PositionOrder = order.PositionOrder;
            email.Send();
        }

        public void SendingOrdersRealizedEmail(Order order)
        {
            OrderRealizedEmail email = new OrderRealizedEmail();
            email.To = order.Email;
            email.From = "Kaczmareek.lukasz@gmail.com";
            email.NumberOrder = order.OrderId;
            email.Send();
        }
    }
}