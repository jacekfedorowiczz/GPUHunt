using FluentValidation;
using FluentValidation.AspNetCore;
using GPUHunt.Application.ApplicationUser;
using GPUHunt.Application.GraphicCard.Commands.CrawlGraphicCards;
using GPUHunt.Application.GraphicCard.Queries.ScrapGraphicCards;
using GPUHunt.Application.Interfaces;
using GPUHunt.Application.Mappings;
using GPUHunt.Application.Services.CardComparer;
using GPUHunt.Application.Services.CardCrawler;
using GPUHunt.Application.Services.CardRemover;
using GPUHunt.Application.Services.CardScraper;
using GPUHunt.Application.Services.CardUpdater;
using GPUHunt.Application.Services.CardValidator;
using GPUHunt.Application.Services.ShopCrawlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IShopCrawler, MoreleCrawler>();
            services.AddScoped<IShopCrawler, XKomCrawler>();
            services.AddScoped<ICardCrawler, CardCrawler>();
            services.AddScoped<ICardComparer, CardComparer>();
            services.AddScoped<ICardUpdater, CardUpdater>();
            services.AddScoped<ICardRemover, CardRemover>();
            services.AddScoped<ICardScraper, CardScraper>();
            services.AddScoped<ICardValidator, CardValidator>();
            services.AddScoped<IUserContext, UserContext>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CrawlGraphicCardsCommand)));


            services.AddAutoMapper(cfg =>
                cfg.AddProfile(new GraphicCardMappingProfile())
            );

            services.AddValidatorsFromAssemblyContaining<ScrapGraphicCardsQueryValidator>()
                    .AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();
        }
    }
}
