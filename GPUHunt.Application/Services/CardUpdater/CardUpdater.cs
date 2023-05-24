﻿using GPUHunt.Application.Interfaces;
using GPUHunt.Application.Models;
using GPUHunt.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.Services.CardUpdater
{
    public class CardUpdater : ICardUpdater
    {
        private readonly IGraphicCardRepository _graphicCardRepository;

        public CardUpdater(IGraphicCardRepository graphicCardRepository)
        {
            _graphicCardRepository = graphicCardRepository ??
                throw new ArgumentNullException(nameof(graphicCardRepository));
        }

        public async Task UpdateGPUPrices(ValidationGraphicCardsModel model)
        {
            try
            {
                if (!await _graphicCardRepository.IsDatabaseNotEmpty() || !model.CardsToUpdate.Any())
                {
                    return;
                }
                else
                {
                    List<Domain.Entities.GraphicCard> graphicCardsToUpdate = new();
                    foreach (var crawledCard in model.CardsToUpdate)
                    {
                        var graphicCardFromDatabase = model.CardsFromDatabase.FirstOrDefault(gpu => gpu.Model.ToLower() == crawledCard.Model.ToLower());
                        if (graphicCardFromDatabase != null)
                        {
                            var result = await UpdateGraphicCard(graphicCardFromDatabase, crawledCard);
                            graphicCardsToUpdate.Add(result);
                        }
                    }

                    foreach (var graphicCardToUpdate in graphicCardsToUpdate)
                    {
                        await _graphicCardRepository.Update(graphicCardToUpdate);
                    }

                    await Task.CompletedTask;
                }
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong.");
            }
        }

        public async Task<Domain.Entities.GraphicCard> UpdateGraphicCard(Domain.Entities.GraphicCard cardFromDatabase, Domain.Entities.GraphicCard crawledGraphicCard)
        {
            try
            {
                cardFromDatabase.LowestPrice = crawledGraphicCard.LowestPrice;
                cardFromDatabase.LowestPriceShop.Name = crawledGraphicCard.LowestPriceShop.Name;
                cardFromDatabase.IsPriceEqual = false;
                if (crawledGraphicCard.LowestPriceShop.Name.Contains(','))
                {
                    cardFromDatabase.HighestPrice = null;
                    cardFromDatabase.HighestPriceShop = null;
                    cardFromDatabase.IsPriceEqual = true;
                }
                else if (crawledGraphicCard.HighestPriceShop != null)
                {
                    cardFromDatabase.HighestPrice = crawledGraphicCard.HighestPrice;
                    cardFromDatabase.HighestPriceShop = crawledGraphicCard.HighestPriceShop;
                }

                return cardFromDatabase;
            }
            catch (NullReferenceException)
            {

                throw new Exception("Something went wrong.");
            }
            catch (ArgumentNullException)
            {

                throw new Exception("Something went wrong.");
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong.");
            }
        }
    }
}