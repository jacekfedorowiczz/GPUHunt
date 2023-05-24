using FluentAssertions;
using GPUHunt.Application.Interfaces;
using GPUHunt.Application.Models;
using GPUHunt.Application.Services.CardValidator;
using GPUHunt.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Tests
{
    public class CardValidatorTests
    {
        [Fact]
        public async Task ValidateGPUs_ForExampleCollection1_ReturnsModelWithoutCardsToUpdate()
        {
            //Arrange
            var mockCardValidator = new Mock<ICardValidator>();
            var mockGraphicCardRepository = new Mock<IGraphicCardRepository>();
            var cardValidator = new CardValidator(mockGraphicCardRepository.Object);

            mockCardValidator.Setup(m => m.ValidateGPUs(GetExampleCollection1()))
                .ReturnsAsync(GetExpectedModel1());

            mockGraphicCardRepository.Setup(m => m.GetAll())
                .ReturnsAsync(new List<Domain.Entities.GraphicCard>());


            //Act
            var result = await cardValidator.ValidateGPUs(GetExampleCollection1());

            //Assert
            result.Should().BeEquivalentTo(GetExpectedModel1());
        }

        [Fact]
        public async Task ValidateGPUs_ForExampleCollection2_ReturnsModelWithCardsToUpdate()
        {
            //Arrange
            var mockCardValidator = new Mock<ICardValidator>();
            var mockGraphicCardRepository = new Mock<IGraphicCardRepository>();
            var cardValidator = new CardValidator(mockGraphicCardRepository.Object);

            mockCardValidator.Setup(m => m.ValidateGPUs(GetExampleCollection2()))
                .ReturnsAsync(GetExpectedModel2());

            mockGraphicCardRepository.Setup(m => m.GetAll())
                .ReturnsAsync(GetCardsFromDatabase());


            //Act
            var result = await cardValidator.ValidateGPUs(GetExampleCollection2());

            //Assert
            result.Should().BeEquivalentTo(GetExpectedModel2());
        }

        private static IEnumerable<Domain.Entities.GraphicCard> GetExampleCollection1()
        {
            return new List<Domain.Entities.GraphicCard>()
            {
                new Domain.Entities.GraphicCard
                    {
                        Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                        LowestPrice = 1499.00M,
                        LowestPriceShop = new Domain.Entities.Shop() { Name = "Morele " },
                        HighestPrice = null,
                        HighestPriceShop = null,
                        IsPriceEqual = false
                    },
                    new Domain.Entities.GraphicCard
                    {
                        Model = "Gigabyte GeForce RTX 3070 Eagle OC 8GB GDDR6x",
                        LowestPrice = 2000.00M,
                        LowestPriceShop = new Domain.Entities.Shop() { Name = "Morele " },
                        HighestPrice = 2100.00M,
                        HighestPriceShop = new Domain.Entities.Shop() { Name = "X-Kom " },
                        IsPriceEqual = false
                    },
                    new Domain.Entities.GraphicCard
                    {
                        Model = "Gigabyte GeForce RTX 3080 Eagle OC 12GB GDDR6X",
                        LowestPrice = 3600.00M,
                        LowestPriceShop = new Domain.Entities.Shop() { Name = "Morele, X-Kom " },
                        HighestPrice = null,
                        HighestPriceShop = null,
                        IsPriceEqual = true
                    }
            };
        }

        private static ValidationGraphicCardsModel GetExpectedModel1()
        {
            return new ValidationGraphicCardsModel
            {
                CardsFromDatabase = new List<Domain.Entities.GraphicCard>(),
                NewCards = new List<Domain.Entities.GraphicCard>()
                {
                    new Domain.Entities.GraphicCard
                    {
                        Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                        LowestPrice = 1499.00M,
                        LowestPriceShop = new Domain.Entities.Shop() { Name = "Morele " },
                        HighestPrice = null,
                        HighestPriceShop = null,
                        IsPriceEqual = false
                    },
                    new Domain.Entities.GraphicCard
                    {
                        Model = "Gigabyte GeForce RTX 3070 Eagle OC 8GB GDDR6x",
                        LowestPrice = 2000.00M,
                        LowestPriceShop = new Domain.Entities.Shop() { Name = "Morele " },
                        HighestPrice = 2100.00M,
                        HighestPriceShop = new Domain.Entities.Shop() { Name = "X-Kom " },
                        IsPriceEqual = false
                    },
                    new Domain.Entities.GraphicCard
                    {
                        Model = "Gigabyte GeForce RTX 3080 Eagle OC 12GB GDDR6X",
                        LowestPrice = 3600.00M,
                        LowestPriceShop = new Domain.Entities.Shop() { Name = "Morele, X-Kom " },
                        HighestPrice = null,
                        HighestPriceShop = null,
                        IsPriceEqual = true
                    }
                },
                CardsToUpdate = null

            };
        }

        private static IEnumerable<Domain.Entities.GraphicCard> GetExampleCollection2()
        {
            return new List<Domain.Entities.GraphicCard>()
            {
                new Domain.Entities.GraphicCard
                {
                    Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                    LowestPrice = 1499.00M,
                    LowestPriceShop = new Domain.Entities.Shop() { Name = "Morele " },
                    HighestPrice = null,
                    HighestPriceShop = null,
                    IsPriceEqual = false
                },
                new Domain.Entities.GraphicCard
                {
                    Model = "Gigabyte GeForce RTX 3070 Eagle OC 8GB GDDR6x",
                    LowestPrice = 2000.00M,
                    LowestPriceShop = new Domain.Entities.Shop() { Name = "Morele " },
                    HighestPrice = 2100.00M,
                    HighestPriceShop = new Domain.Entities.Shop() { Name = "X-Kom " },
                    IsPriceEqual = false
                },
                new Domain.Entities.GraphicCard
                {
                    Model = "Gigabyte GeForce RTX 3080 Eagle OC 12GB GDDR6X",
                    LowestPrice = 3600.00M,
                    LowestPriceShop = new Domain.Entities.Shop() { Name = "Morele, X-Kom " },
                    HighestPrice = null,
                    HighestPriceShop = null,
                    IsPriceEqual = true
                }
            };
        }

        private static IEnumerable<Domain.Entities.GraphicCard> GetCardsFromDatabase()
        {
            return new List<Domain.Entities.GraphicCard>
            {
                new Domain.Entities.GraphicCard
                {
                    Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                    LowestPrice = 1599.00M,
                    LowestPriceShop = new Domain.Entities.Shop() { Name = "Morele " },
                    HighestPrice = null,
                    HighestPriceShop = null,
                    IsPriceEqual = false
                },
                new Domain.Entities.GraphicCard
                {
                    Model = "Gigabyte GeForce RTX 3070 Eagle OC 8GB GDDR6x",
                    LowestPrice = 2200.00M,
                    LowestPriceShop = new Domain.Entities.Shop() { Name = "X-Kom " },
                    HighestPrice = 2300.00M,
                    HighestPriceShop = new Domain.Entities.Shop() { Name = "Morele " },
                    IsPriceEqual = false
                },
            };
        }

        private static ValidationGraphicCardsModel GetExpectedModel2()
        {
            return new ValidationGraphicCardsModel
            {
                CardsFromDatabase = new List<Domain.Entities.GraphicCard>()
                {
                    new Domain.Entities.GraphicCard
                    {
                        Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                        LowestPrice = 1599.00M,
                        LowestPriceShop = new Domain.Entities.Shop() { Name = "Morele " },
                        HighestPrice = null,
                        HighestPriceShop = null,
                        IsPriceEqual = false
                    },
                    new Domain.Entities.GraphicCard
                    {
                        Model = "Gigabyte GeForce RTX 3070 Eagle OC 8GB GDDR6x",
                        LowestPrice = 2200.00M,
                        LowestPriceShop = new Domain.Entities.Shop() { Name = "X-Kom " },
                        HighestPrice = 2300.00M,
                        HighestPriceShop = new Domain.Entities.Shop() { Name = "Morele " },
                        IsPriceEqual = false
                    },
                },
                NewCards = new List<Domain.Entities.GraphicCard>()
                {
                    new Domain.Entities.GraphicCard
                    {
                        Model = "Gigabyte GeForce RTX 3080 Eagle OC 12GB GDDR6X",
                        LowestPrice = 3600.00M,
                        LowestPriceShop = new Domain.Entities.Shop() { Name = "Morele, X-Kom " },
                        HighestPrice = null,
                        HighestPriceShop = null,
                        IsPriceEqual = true
                    }
                },
                CardsToUpdate = new List<Domain.Entities.GraphicCard>()
                {
                    new Domain.Entities.GraphicCard
                    {
                        Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                        LowestPrice = 1499.00M,
                        LowestPriceShop = new Domain.Entities.Shop() { Name = "Morele " },
                        HighestPrice = null,
                        HighestPriceShop = null,
                        IsPriceEqual = false
                    },
                    new Domain.Entities.GraphicCard
                    {
                        Model = "Gigabyte GeForce RTX 3070 Eagle OC 8GB GDDR6x",
                        LowestPrice = 2000.00M,
                        LowestPriceShop = new Domain.Entities.Shop() { Name = "Morele " },
                        HighestPrice = 2100.00M,
                        HighestPriceShop = new Domain.Entities.Shop() { Name = "X-Kom " },
                        IsPriceEqual = false
                    },
                }
            };
        }
    }
}
