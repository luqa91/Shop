using Hangfire;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Infrastructure
{
    public class HangFirePostalMailService: IMailService
    {
        public void SendingConfirmationOrdersEmail(Order order)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            string url = urlHelper.Action("SendingConfirmationOrdersEmail", "Manage", new { orderId = order.OrderId, lastname = order.LastName }, HttpContext.Current.Request.Url.Scheme);

            BackgroundJob.Enqueue(() => Helpers.CallUrl(url));
        }

        public void SendingOrdersRealizedEmail(Order order)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            string url = urlHelper.Action("SendingOrdersRealizedEmail", "Manage", new { orderId = order.OrderId, lastname = order.LastName }, HttpContext.Current.Request.Url.Scheme);

            BackgroundJob.Enqueue(() => Helpers.CallUrl(url));
        }
    }
}