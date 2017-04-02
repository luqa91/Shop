namespace Shop.Models
{
    public class PositionOrder
    {

        public int PositionOrderId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePurchase { get; set; }

        public virtual Product product { get; set; }
        public virtual Order order { get; set; }


    }
}