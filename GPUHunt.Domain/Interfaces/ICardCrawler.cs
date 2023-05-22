using GPUHunt.Domain.Entities;
using GPUHunt.Domain.Models;

namespace GPUHunt.Domain.Interfaces
{
    public interface ICardCrawler
    {
        Task<IEnumerable<GraphicCard>> Crawl();
        Task<ValidationGraphicCardsModel> GetValidationModel();
    }
}
