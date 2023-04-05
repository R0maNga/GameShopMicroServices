namespace MainService.Models.Response.GameResponse
{
    public class GetGameResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
