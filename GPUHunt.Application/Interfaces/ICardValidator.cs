using GPUHunt.Application.Models;
using GPUHunt.Domain.Entities;

namespace GPUHunt.Application.Interfaces
{
    public interface ICardValidator
    {
        Task<ValidationGraphicCardsModel> ValidateGPUs(IEnumerable<GraphicCard> comparedCards);
    }
}
