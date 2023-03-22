namespace OrderService.Models.Response.OrderResponse
{
    public class GetOrderResponse
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }
        public int BasketId { get; set; }

        public decimal Price { get; set; }
    }
}
