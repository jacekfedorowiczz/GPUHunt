using GPUHunt.Domain.Entities;

namespace GPUHunt.Domain.Interfaces
{
    public interface ICardRemover
    {
        Task Remove(IEnumerable<GraphicCard> comparedCards, IEnumerable<GraphicCard> cardsFromDatabase);
    }
}
