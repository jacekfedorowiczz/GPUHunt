using GPUHunt.Application.Interfaces;
using GPUHunt.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.Services.CardRemover
{
    public class CardRemover : ICardRemover
    {
        private readonly IGraphicCardRepository _graphicCardRepository;

        public CardRemover(IGraphicCardRepository graphicCardRepository)
        {
            _graphicCardRepository = graphicCardRepository ??
                throw new ArgumentNullException(nameof(graphicCardRepository));
        }

        public async Task Remove(IEnumerable<Domain.Entities.GraphicCard> comparedCards, IEnumerable<Domain.Entities.GraphicCard> cardsFromDatabase)
        {
            foreach (var gpu in cardsFromDatabase)
            {
                var sameCard = comparedCards.FirstOrDefault(sc => sc.Model.ToLower() == gpu.Model.ToLower());
                if (sameCard == null)
                {
                    await _graphicCardRepository.Delete(gpu.Id);
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
