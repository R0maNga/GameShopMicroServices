namespace MainService.Models.Response.BasketToGameResponse
{
    public class BasketToGameResponse
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int GameId { get; set; }
        public GameForBasketToGameResponse Price { get; set; }
    }
}
