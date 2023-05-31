namespace GPUHunt.Domain.Entities
{
    public class GraphicCard
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        public decimal? MorelePrice { get; set; }
        public decimal? XKomPrice { get; set; }
        public bool IsPriceEqual { get; set; }

        public decimal LowestPrice { get; set; }
        public Store LowestPriceStore { get; set; }
        public decimal? HighestPrice { get; set; }
        public Store? HighestPriceStore { get; set; }
    }
}
