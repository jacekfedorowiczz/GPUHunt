using GPUHunt.Domain.Entities;

namespace GPUHunt.Application.Models
{
    public class ValidationGraphicCardsModel
    {
        public IEnumerable<Domain.Entities.GraphicCard> CardsFromDatabase { get; set; } = default!;
        public IEnumerable<Domain.Entities.GraphicCard> NewCards { get; set; } = default!;
        public IEnumerable<Domain.Entities.GraphicCard>? CardsToUpdate { get; set; }
    }
}
