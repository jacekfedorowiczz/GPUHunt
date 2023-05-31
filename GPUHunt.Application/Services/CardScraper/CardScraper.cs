using AutoMapper;
using GPUHunt.Application.Interfaces;
using GPUHunt.Application.Models.DTOs;
using GPUHunt.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.Services.CardScraper
{
    public class CardScraper : ICardScraper
    {
        private readonly IGraphicCardRepository _graphicCardRepository;
        private readonly IMapper _mapper;

        public CardScraper(IGraphicCardRepository graphicCardRepository, IMapper mapper)
        {
            _graphicCardRepository = graphicCardRepository ??
                throw new ArgumentNullException(nameof(graphicCardRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<string> Scrap()
        {
            try
            {
                // dodaj parametr do query określający format 
                // dodaj słownik przeszkujący w dynamiczny sposób typów z użyciem kontenera DI




                JsonSerializerSettings config = new() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, };

                var gpus = await _graphicCardRepository.GetAll();
                var dtos = _mapper.Map<IEnumerable<GraphicCardDto>>(gpus);
                var json = JsonConvert.SerializeObject(dtos, config);

                return json;
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong");
            }
        }
    }
}
