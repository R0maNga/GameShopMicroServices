namespace MainService.Models.Request.BasketRequest
{
    public class UpdateBasketRequest
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public int UserId { get; set; }
    }
}
