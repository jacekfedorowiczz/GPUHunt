using GPUHunt.Application.Interfaces;
using GPUHunt.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.GraphicCard.Commands.CrawlGraphicCards
{
    public class CrawlGraphicCardsCommandHandler : IRequestHandler<CrawlGraphicCardsCommand>
    {
        private readonly IGraphicCardRepository _graphicCardRepository;
        private readonly ICardCrawler _crawler;

        public CrawlGraphicCardsCommandHandler(IGraphicCardRepository graphicCardRepository, ICardCrawler crawler)
        {
            _graphicCardRepository = graphicCardRepository ??
                throw new ArgumentNullException(nameof(graphicCardRepository));
            _crawler = crawler ??
                throw new ArgumentNullException(nameof(crawler));
        }

        public async Task Handle(CrawlGraphicCardsCommand request, CancellationToken cancellationToken)
        {
            var gpus = await _crawler.Crawl();
            await _graphicCardRepository.Crawl(gpus);
        }
    }
}
