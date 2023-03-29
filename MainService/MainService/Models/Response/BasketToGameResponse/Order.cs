namespace MainService.Models.Response.BasketToGameResponse
{
    public class Order
    {
        public string OrderStatus { get; set; }
        public int BasketId { get; set; }

        public decimal Price { get; set; }
    }
}
