namespace MainService.Models.Request.BasketToGameRequest
{
    public class UpdateBasketToGameRequest
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int GameId { get; set; }
    }
}
