namespace MainService.Models.Request.GemeRequest
{
    public class CreateGameRequest
    {
        
            public string Name { get; set; }
            public string Discription { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }

    }
}
