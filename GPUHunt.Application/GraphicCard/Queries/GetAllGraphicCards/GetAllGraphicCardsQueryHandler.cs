using AutoMapper;
using GPUHunt.Application.Models.DTOs;
using GPUHunt.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.GraphicCard.Queries.GetAllGraphicCards
{
    public class GetAllGraphicCardsQueryHandler : IRequestHandler<GetAllGraphicCardsQuery, IEnumerable<GraphicCardDto>>
    {
        private readonly IGraphicCardRepository _graphicCardRepository;
        private readonly IMapper _mapper;

        public GetAllGraphicCardsQueryHandler(IGraphicCardRepository graphicCardRepository, IMapper mapper)
        {
            _graphicCardRepository = graphicCardRepository ??
                throw new ArgumentNullException(nameof(graphicCardRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<GraphicCardDto>> Handle(GetAllGraphicCardsQuery request, CancellationToken cancellationToken)
        {
            var cards = await _graphicCardRepository.GetAll();
            var list = new List<GraphicCardDto>();
            foreach (var card in cards)
            {
                var dto = _mapper.Map<GraphicCardDto>(card);
                list.Add(dto);
            }

            var result = list.OrderBy(r => r.Model);

            return result;
        }
    }
}
