namespace OrderService.Models.Request.OrderRequest
{
    public class CreateOrderRequest
    {
        public string OrderStatus { get; set; }
        public int BasketId { get; set; }

        public decimal Price { get; set; }
    }
}
