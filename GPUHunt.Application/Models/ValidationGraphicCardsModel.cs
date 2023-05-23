using GPUHunt.Domain.Entities;

namespace GPUHunt.Application.Models
{
    public class ValidationGraphicCardsModel
    {
        public IEnumerable<GraphicCard> CardsFromDatabase { get; set; } = default!;
        public IEnumerable<GraphicCard> NewCards { get; set; } = default!;
        public IEnumerable<GraphicCard>? CardsToUpdate { get; set; }
    }
}
