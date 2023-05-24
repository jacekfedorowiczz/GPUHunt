using AutoMapper;
using GPUHunt.Application.Models.DTOs;
using GPUHunt.Domain.Interfaces;
using GPUHunt.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.GraphicCard.Queries.GetPaginatedGraphicCards
{
    public class GetPaginatedGraphicCardsQueryHandler : IRequestHandler<GetPaginatedGraphicCardsQuery, PagedResult<GraphicCardDto>>
    {
        private readonly IGraphicCardRepository _graphicCardRepository;
        private readonly IMapper _mapper;

        public GetPaginatedGraphicCardsQueryHandler(IGraphicCardRepository graphicCardRepository, IMapper mapper)
        {
            _graphicCardRepository = graphicCardRepository ??
                throw new ArgumentNullException(nameof(graphicCardRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PagedResult<GraphicCardDto>> Handle(GetPaginatedGraphicCardsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var graphicCards = await _graphicCardRepository.GetPaginatedCards(request.Query);
                var dtos = new List<GraphicCardDto>();

                foreach (var graphicCard in graphicCards.Items)
                {
                    var dto = _mapper.Map<GraphicCardDto>(graphicCard);
                    dtos.Add(dto);
                }

                return new PagedResult<GraphicCardDto>(dtos, graphicCards.TotalItemsCount, request.Query.PageSize, request.Query.PageNumber);
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong.");
            }
        }
    }
}
