using Shop.Models;

namespace Shop.Infrastructure
{
    public interface IMailService
    {
        void SendingConfirmationOrdersEmail (Order order);
        void SendingOrdersRealizedEmail(Order order);
    }
}
