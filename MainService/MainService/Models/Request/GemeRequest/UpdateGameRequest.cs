namespace MainService.Models.Request.GemeRequest
{
    public class UpdateGameRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public decimal Prie { get; set; }
        public int Quantity { get; set; }
    }
}
