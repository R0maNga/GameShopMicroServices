namespace GameService.Models.GameStorageRequest
{
    public class UpdateGameStorageRequest
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int SoldGames { get; set; }
        public int GamesInStorage { get; set; }
    }
}
