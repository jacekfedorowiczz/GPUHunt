using GPUHunt.Application.Interfaces;
using GPUHunt.Application.Models;
using GPUHunt.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.Services.CardValidator
{
    public class CardValidator : ICardValidator
    {
        private readonly IGraphicCardRepository _graphicCardRepository;

        public CardValidator(IGraphicCardRepository graphicCardRepository)
        {
            _graphicCardRepository = graphicCardRepository ??
                throw new ArgumentNullException(nameof(graphicCardRepository));
        }

        public async Task<ValidationGraphicCardsModel> ValidateGPUs(IEnumerable<Domain.Entities.GraphicCard> comparedCards)
        {
            try
            {
                List<Domain.Entities.GraphicCard> newCards = new();
                List<Domain.Entities.GraphicCard> cardsToUpdate = new();
                var gpusFromDatabase = await _graphicCardRepository.GetAll();

                if (gpusFromDatabase.Any())
                {
                    foreach (var card in comparedCards)
                    {
                        var sameCardFromDatabase = gpusFromDatabase.FirstOrDefault(sc => sc.Model.ToLower() == card.Model.ToLower());
                        if (sameCardFromDatabase == null)
                        {
                            newCards.Add(card);
                        }
                        else
                        {
                            cardsToUpdate.Add(card);
                        }
                    }

                    return new ValidationGraphicCardsModel()
                    {
                        CardsFromDatabase = gpusFromDatabase,
                        NewCards = newCards,
                        CardsToUpdate = cardsToUpdate
                    };
                }
                else
                {
                    return new ValidationGraphicCardsModel()
                    {
                        CardsFromDatabase = gpusFromDatabase,
                        NewCards = comparedCards,
                        CardsToUpdate = null
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
