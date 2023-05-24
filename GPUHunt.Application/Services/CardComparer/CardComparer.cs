using GPUHunt.Application.Interfaces;
using GPUHunt.Application.Models;
using GPUHunt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.Services.CardComparer
{
    public class CardComparer : ICardComparer
    {
        public CardComparer()
        {

        }

        public async Task<IEnumerable<Domain.Entities.GraphicCard>> Compare(IEnumerable<GPU> gpus)
        {
            var graphicCards = new List<Domain.Entities.GraphicCard>();

            foreach (var gpu in gpus)
            {
                if (graphicCards.FirstOrDefault(gc => gc.Model.ToLower() == gpu.Model.ToLower()) == null)
                {
                    var sameGpu = gpus.FirstOrDefault(sg => sg.Model.ToLower() == gpu.Model.ToLower() && sg.Shop.ToLower() != gpu.Shop.ToLower());

                    if (sameGpu == null)
                    {
                        var entity = await CreateGraphicCardWithoutCompare(gpu);
                        graphicCards.Add(entity);
                    }
                    else
                    {
                        var entity = await CreateGraphicCardAfterCompare(gpu, sameGpu);
                        graphicCards.Add(entity);
                    }
                }
            }

            return graphicCards;
        }

        private static async Task<Domain.Entities.GraphicCard> CreateGraphicCardAfterCompare(GPU gpu, GPU sameGpu)
        {
            var graphicCard = new Domain.Entities.GraphicCard();
            StringBuilder sb = new(gpu.Shop);

            graphicCard.Model = gpu.Model;
            graphicCard.Vendor = gpu.Vendor;
            graphicCard.IsPriceEqual = false;

            if (gpu.Price > sameGpu.Price)
            {
                graphicCard.LowestPrice = sameGpu.Price;
                graphicCard.LowestPriceShop = new Shop() { Name = $"{sameGpu.Shop}" };
                graphicCard.HighestPrice = gpu.Price;
                graphicCard.HighestPriceShop = new Shop() { Name = $"{gpu.Shop}" };
            }
            else if (gpu.Price < sameGpu.Price)
            {
                graphicCard.LowestPrice = gpu.Price;
                graphicCard.LowestPriceShop = new Shop() { Name = $"{gpu.Shop}" };
                graphicCard.HighestPrice = sameGpu.Price;
                graphicCard.HighestPriceShop = new Shop() { Name = $"{sameGpu.Shop}" };
            }
            else
            {
                graphicCard.LowestPrice = sameGpu.Price;
                graphicCard.LowestPriceShop = new Shop() { Name = $"{sb.Append($", {sameGpu.Shop}")}" };
                graphicCard.HighestPrice = null;
                graphicCard.HighestPriceShop = null;
                graphicCard.IsPriceEqual = true;
            }

            return graphicCard;
        }

        private static async Task<Domain.Entities.GraphicCard> CreateGraphicCardWithoutCompare(GPU gpu)
        {
            return new Domain.Entities.GraphicCard()
            {
                Model = gpu.Model,
                Vendor = gpu.Vendor,
                LowestPrice = gpu.Price,
                LowestPriceShop = new Shop() { Name = gpu.Shop },
                HighestPrice = null,
                HighestPriceShop = null,
                IsPriceEqual = false
            };
        }
    }
}
