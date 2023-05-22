using GPUHunt.Domain.Entities;
using GPUHunt.Domain.Models;

namespace GPUHunt.Domain.Interfaces
{
    public interface ICardValidator
    {
        Task<ValidationGraphicCardsModel> ValidateGPUs(IEnumerable<GraphicCard> comparedCards);
    }
}
