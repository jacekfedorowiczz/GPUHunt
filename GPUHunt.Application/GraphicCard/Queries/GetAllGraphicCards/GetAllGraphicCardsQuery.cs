using GPUHunt.Application.Models.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.GraphicCard.Queries.GetAllGraphicCards
{
    public class GetAllGraphicCardsQuery : GraphicCardDto, IRequest<IEnumerable<GraphicCardDto>>
    {
    }
}
