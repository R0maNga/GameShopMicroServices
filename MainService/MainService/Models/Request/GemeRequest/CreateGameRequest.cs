namespace MainService.Models.Request.GemeRequest
{
    public class CreateGameRequest
    {
        
            public string Name { get; set; }
            public string Discription { get; set; }
            public decimal Prie { get; set; }
            public int Quantity { get; set; }

    }
}
