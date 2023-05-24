using AutoMapper;
using GPUHunt.Application.Models;
using GPUHunt.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.Mappings
{
    public class GraphicCardMappingProfile : Profile
    {
        public GraphicCardMappingProfile()
        {
            CreateMap<GPU, Domain.Entities.GraphicCard>()
                .ForMember(gc => gc.LowestPrice, opt => opt.MapFrom(g => g.Price))
                .ForMember(gc => gc.LowestPriceShop, opt => opt.MapFrom(g => g.Shop));

            CreateMap<Domain.Entities.GraphicCard, GraphicCardDto>()
                .ForMember(dto => dto.LowestPriceShop, opt => opt.MapFrom(gc => gc.LowestPriceShop.Name))
                .ForMember(dto => dto.HighestPriceShop, opt => opt.MapFrom(gc => gc.HighestPriceShop.Name));
        }
    }
}
