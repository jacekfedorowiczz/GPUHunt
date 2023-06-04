using GPUHunt.Application.Interfaces;
using GPUHunt.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.Services.CardCrawler
{
    public class CardCrawler : ICardCrawler
    {
        private readonly IEnumerable<IShopCrawler> _shopCrawlers;
        private readonly ICardComparer _comparer;
        private readonly ICardValidator _validator;
        private readonly ICardRemover _remover;
        private readonly ICardUpdater _updater;

        public CardCrawler(IEnumerable<IShopCrawler> shopCrawlers, ICardComparer comparer, ICardValidator cardValidator, ICardRemover remover, ICardUpdater updater)
        {
            _shopCrawlers = shopCrawlers ??
                throw new ArgumentNullException(nameof(shopCrawlers));
            _comparer = comparer ??
                throw new ArgumentNullException(nameof(comparer));
            _validator = cardValidator ??
                throw new ArgumentNullException(nameof(cardValidator));
            _remover = remover ??
                throw new ArgumentNullException(nameof(remover));
            _updater = updater ??
                throw new ArgumentNullException(nameof(updater));
        }

        public async Task<IEnumerable<Domain.Entities.GraphicCard>> Crawl()
        {
            try
            {
                var model = await CrawlToModel();
                await _updater.UpdateGPUPrices(model);

                return model.NewCards;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<ValidationGraphicCardsModel> CrawlToModel()
        {
            List<GPU> gpus = new();

            foreach (var crawler in _shopCrawlers)
            {
                var crawledCards = await crawler.CrawlShop();
                gpus.AddRange(crawledCards);
            }

            var comparedCards = await _comparer.Compare(gpus);
            var validatedCards = await _validator.ValidateGPUs(comparedCards);
            await _remover.Remove(comparedCards, validatedCards.CardsFromDatabase);

            return validatedCards;
        }
    }
}
