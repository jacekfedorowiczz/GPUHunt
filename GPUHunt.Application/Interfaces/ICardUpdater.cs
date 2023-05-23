using GPUHunt.Application.Models;
using GPUHunt.Domain.Entities;

namespace GPUHunt.Application.Interfaces
{
    public interface ICardUpdater
    {
        Task UpdateGPUPrices(ValidationGraphicCardsModel model);
        Task<GraphicCard> UpdateGraphicCard(GraphicCard cardFromDatabase, GraphicCard graphicCard);
    }
}
