using GPUHunt.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.GraphicCard.Queries.IsDatabaseNotEmpty
{
    public class IsDatabaseNotEmptyQueryHandler : IRequestHandler<IsDatabaseNotEmptyQuery, bool>
    {
        private readonly IGraphicCardRepository _graphicCardRepository;

        public IsDatabaseNotEmptyQueryHandler(IGraphicCardRepository graphicCardRepository)
        {
            _graphicCardRepository = graphicCardRepository;
        }

        public async Task<bool> Handle(IsDatabaseNotEmptyQuery request, CancellationToken cancellationToken)
        {
            var isDatabaseEmpty = await _graphicCardRepository.IsDatabaseNotEmpty();
            return isDatabaseEmpty;
        }
    }
}