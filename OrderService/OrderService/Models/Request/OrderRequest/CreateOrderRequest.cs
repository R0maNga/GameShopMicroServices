namespace OrderService.Models.Request.OrderRequest
{
    public class CreateOrderRequest
    {
        public decimal Price { get; set; }
        public int BasketId { get; set; }
        public string OrderStatus { get; set; }
    }
}
