using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.GraphicCard.Queries.ScrapGraphicCards
{
    public class ScrapGraphicCardsQueryValidator : AbstractValidator<ScrapGraphicCardsQuery>
    {
        public ScrapGraphicCardsQueryValidator()
        {
            RuleFor(gpu => gpu.Vendor)
                .NotEmpty().WithMessage("GraphicCard must have a vendor.");

            RuleFor(gpu => gpu.Model)
                .NotEmpty().WithMessage("GraphicCard must have a model.");

            RuleFor(gpu => gpu.LowestPrice)
                .NotEmpty().WithMessage("GraphicCard must have a lowest available price");

            RuleFor(gpu => gpu.LowestPriceShop)
                .NotEmpty().WithMessage("GraphicCard must have a shop that offers the lowest price.");
        }
    }
}
