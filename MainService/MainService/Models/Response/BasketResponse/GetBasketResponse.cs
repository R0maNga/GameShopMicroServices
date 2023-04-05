namespace MainService.Models.Response.BasketResponse
{
    public class GetBasketResponse
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public int UserId { get; set; }
    }
}
