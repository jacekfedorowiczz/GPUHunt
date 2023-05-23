using GPUHunt.Domain.Entities;

namespace GPUHunt.Application.Interfaces
{
    public interface ICardRemover
    {
        Task Remove(IEnumerable<GraphicCard> comparedCards, IEnumerable<GraphicCard> cardsFromDatabase);
    }
}
