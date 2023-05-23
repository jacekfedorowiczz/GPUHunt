namespace GPUHunt.Domain.Entities
{
    public class GraphicCard
    {
        public int Id { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public decimal LowestPrice { get; set; }
        public Shop LowestPriceShop { get; set; }
        public decimal? HighestPrice { get; set; }
        public Shop? HighestPriceShop { get; set; }
        public bool IsPriceEqual { get; set; }
    }
}
