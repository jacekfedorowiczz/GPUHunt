using GPUHunt.Application.Models;

namespace GPUHunt.Application.Interfaces
{
    public interface IShopCrawler
    {
        Task<IEnumerable<GPU>> CrawlShop();
    }
}
