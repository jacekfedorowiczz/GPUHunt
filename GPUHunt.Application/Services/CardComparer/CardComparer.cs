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
                    var sameGpu = gpus.FirstOrDefault(sg => sg.Model.ToLower() == gpu.Model.ToLower() && sg.Store.ToLower() != gpu.Store.ToLower());

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
            StringBuilder sb = new(gpu.Store);

            graphicCard.Model = gpu.Model;

            switch (gpu.Vendor)
            {
                case Models.Enums.Vendors.NVIDIA:
                    graphicCard.VendorId = 1;
                    break;
                case Models.Enums.Vendors.AMD:
                    graphicCard.VendorId = 2;
                    break;
                case Models.Enums.Vendors.Intel:
                    graphicCard.VendorId = 3;
                    break;
                default:
                    graphicCard.VendorId = 4;
                    break;
            }

            if (gpu.Store == "Morele")
            {
                graphicCard.MorelePrice = gpu.Price;
                graphicCard.XKomPrice = sameGpu.Price;
            }
            else
            {
                graphicCard.MorelePrice = sameGpu.Price;
                graphicCard.XKomPrice = gpu.Price;
            }

            if (gpu.Price != sameGpu.Price)
            {
                switch (gpu.Price > sameGpu.Price)
                {
                    case true:
                    graphicCard.LowestPrice = sameGpu.Price;
                    graphicCard.LowestPriceStore = sameGpu.Store;
                    graphicCard.HighestPrice = gpu.Price;
                    graphicCard.HighestPriceStore = gpu.Store;
                    break;
                case false:
                    graphicCard.LowestPrice = gpu.Price;
                    graphicCard.LowestPriceStore = gpu.Store;
                    graphicCard.HighestPrice = sameGpu.Price;
                    graphicCard.HighestPriceStore = sameGpu.Store;
                    break;
            }
            }
            else
            {
                graphicCard.LowestPrice = gpu.Price;
                graphicCard.LowestPriceStore = sb.Append(", X-Kom").ToString();
                graphicCard.IsPriceEqual = true;
            }

            return graphicCard;
        }

        private static async Task<Domain.Entities.GraphicCard> CreateGraphicCardWithoutCompare(GPU gpu)
        {
            var graphicCard = new Domain.Entities.GraphicCard()
            {
                Model = gpu.Model,
                IsPriceEqual = false
            };

            switch (gpu.Vendor)
            {
                case Models.Enums.Vendors.NVIDIA:
                    graphicCard.VendorId = 1;
                    break;
                case Models.Enums.Vendors.AMD:
                    graphicCard.VendorId = 2;
                    break;
                case Models.Enums.Vendors.Intel:
                    graphicCard.VendorId = 3;
                    break;
                default:
                    graphicCard.VendorId = 4;
                    break;
            }

            if (gpu.Store == "Morele")
            {
                graphicCard.MorelePrice = gpu.Price;
                graphicCard.LowestPrice = gpu.Price;
                graphicCard.LowestPriceStore = "Morele";
            }
            else
            {
                graphicCard.XKomPrice = gpu.Price;
                graphicCard.LowestPrice = gpu.Price;
                graphicCard.LowestPriceStore = "X-kom";
            }

            return graphicCard;
        }
    }
}
