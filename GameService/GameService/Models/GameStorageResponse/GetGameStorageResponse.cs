namespace GameService.Models.GameStorageResponse
{
    public class GetGameStorageResponse
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int SoldGames { get; set; }
        public int GamesInStorage { get; set; }
    }
}
