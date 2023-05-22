namespace GPUHunt.Domain.Interfaces
{
    public interface ICardScraper
    {
        Task<string> Scrap();
    }
}
