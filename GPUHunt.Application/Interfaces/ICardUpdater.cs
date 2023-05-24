using GPUHunt.Application.Models;
using GPUHunt.Domain.Entities;

namespace GPUHunt.Application.Interfaces
{
    public interface ICardUpdater
    {
        Task UpdateGPUPrices(ValidationGraphicCardsModel model);
        Task<Domain.Entities.GraphicCard> UpdateGraphicCard(Domain.Entities.GraphicCard cardFromDatabase, Domain.Entities.GraphicCard graphicCard);
    }
}
