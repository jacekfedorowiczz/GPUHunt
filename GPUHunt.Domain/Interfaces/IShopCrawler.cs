using GPUHunt.Domain.Models;

namespace GPUHunt.Domain.Interfaces
{
    public interface IShopCrawler
    {
        Task<IEnumerable<GPU>> CrawlShop();
    }
}
