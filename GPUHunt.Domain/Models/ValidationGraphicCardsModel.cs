using GPUHunt.Domain.Entities;

namespace GPUHunt.Domain.Models
{
    public class ValidationGraphicCardsModel
    {
        public IEnumerable<GraphicCard> CardsFromDatabase { get; set; } = default!;
        public IEnumerable<GraphicCard> NewCards { get; set; } = default!;
        public IEnumerable<GraphicCard>? CardsToUpdate { get; set; }
    }
}
