namespace MainService.Models.Request.BasketRequest
{
    public class CreateBasketRequest
    {
        public double TotalPrice { get; set; }
        public int UserId { get; set; }
    }
}
