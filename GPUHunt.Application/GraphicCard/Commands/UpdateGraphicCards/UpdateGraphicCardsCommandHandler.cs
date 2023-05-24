using GPUHunt.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.GraphicCard.Commands.UpdateGraphicCards
{
    public class UpdateGraphicCardsCommandHandler : IRequestHandler<UpdateGraphicCardsCommand>
    {
        private readonly ICardUpdater _updater;
        private readonly ICardCrawler _crawler;

        public UpdateGraphicCardsCommandHandler(ICardUpdater updater, ICardCrawler crawler)
        {
            _updater = updater ??
                throw new ArgumentNullException(nameof(updater));
            _crawler = crawler;
        }

        public async Task Handle(UpdateGraphicCardsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = await _crawler.CrawlToModel();
                await _updater.UpdateGPUPrices(model);
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong.");
            }
        }
    }
}
