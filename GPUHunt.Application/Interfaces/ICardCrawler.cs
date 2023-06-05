using GPUHunt.Application.Models;
using GPUHunt.Domain.Entities;

namespace GPUHunt.Application.Interfaces
{
    public interface ICardCrawler
    {
        //Task<IEnumerable<Domain.Entities.GraphicCard>> Crawl();
        Task<ValidationGraphicCardsModel> CrawlToModel();
    }
}
