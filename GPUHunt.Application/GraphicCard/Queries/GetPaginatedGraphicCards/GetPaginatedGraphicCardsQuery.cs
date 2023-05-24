using GPUHunt.Application.Models.DTOs;
using GPUHunt.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.GraphicCard.Queries.GetPaginatedGraphicCards
{
    public class GetPaginatedGraphicCardsQuery : GraphicCardDto, IRequest<PagedResult<GraphicCardDto>>
    {
        public GraphicCardQuery Query { get; }

        public GetPaginatedGraphicCardsQuery(GraphicCardQuery query)
        {
            Query = query;
        }
    }
}
