using GPUHunt.Domain.Entities;
using GPUHunt.Domain.Models;

namespace GPUHunt.Domain.Interfaces
{
    public interface ICardUpdater
    {
        Task UpdateGPUPrices(ValidationGraphicCardsModel model);
        Task<GraphicCard> UpdateGraphicCard(GraphicCard cardFromDatabase, GraphicCard graphicCard);
    }
}
