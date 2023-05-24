using GPUHunt.Domain.Entities;

namespace GPUHunt.Application.Interfaces
{
    public interface ICardRemover
    {
        Task Remove(IEnumerable<Domain.Entities.GraphicCard> comparedCards, IEnumerable<Domain.Entities.GraphicCard> cardsFromDatabase);
    }
}
