namespace OrderService.Models.Request.OrderRequest
{
    public class UpdateOrderRequest
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }
        public int BasketId { get; set; }

        public decimal Price { get; set; }
    }
}
