using FluentAssertions;
using GPUHunt.Application.Interfaces;
using GPUHunt.Application.Services.CardUpdater;
using GPUHunt.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Tests
{
    public class CardUpdaterTests
    {
        [Fact]
        public async Task UpdateGraphicCard_ForExampleCard_ReturnsCardWithoutHighestPrice()
        {
            //Arrange
            var mockCardUpdater = new Mock<ICardUpdater>();
            var mockGraphicCardRepository = new Mock<IGraphicCardRepository>();
            var cardUpdater = new CardUpdater(mockGraphicCardRepository.Object);
            var expectedCard = new Domain.Entities.GraphicCard()
            {
                Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                VendorId = 1,
                MorelePrice = 1599.00M,
                XKomPrice = null,
                LowestPrice = 1599.00M,
                LowestPriceStore = "Morele",
                HighestPrice = null,
                HighestPriceStore = null,
                IsPriceEqual = false
            };


            mockCardUpdater.Setup(m => m.UpdateGraphicCard(GetExampleCardFromDatabase1(), GetExampleCrawledCardWithoutHighestPrice()))
                .ReturnsAsync(() => expectedCard);

            //Act
            var result = await cardUpdater.UpdateGraphicCard(GetExampleCardFromDatabase1(), GetExampleCrawledCardWithoutHighestPrice());

            //Assert
            result.Should().BeEquivalentTo(expectedCard);
        }

        [Fact]
        public async Task UpdateGraphicCard_ForExampleCard_ReturnsCardWithHighestPrice()
        {
            //Arrange
            var mockCardUpdater = new Mock<ICardUpdater>();
            var mockGraphicCardRepository = new Mock<IGraphicCardRepository>();
            var cardUpdater = new CardUpdater(mockGraphicCardRepository.Object);
            var expectedCard = new Domain.Entities.GraphicCard()
            {
                Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                VendorId = 1,
                MorelePrice = 1599.00M,
                XKomPrice = 1799.00M,
                LowestPrice = 1599.00M,
                LowestPriceStore = "Morele",
                HighestPrice = 1799.00M,
                HighestPriceStore = "X-Kom",
                IsPriceEqual = false
            };

            mockCardUpdater.Setup(m => m.UpdateGraphicCard(GetExampleCardFromDatabase1(), GetExampleCrawledCardWithHighestPrice()))
                .ReturnsAsync(() => expectedCard);

            //Act
            var result = await cardUpdater.UpdateGraphicCard(GetExampleCardFromDatabase1(), GetExampleCrawledCardWithHighestPrice());

            //Assert
            result.Should().BeEquivalentTo(expectedCard);
        }

        [Fact]
        public async Task UpdateGraphicCard_ForExampleCard_ReturnsCardWithEqualPrice()
        {
            //Arrange
            var mockCardUpdater = new Mock<ICardUpdater>();
            var mockGraphicCardRepository = new Mock<IGraphicCardRepository>();
            var cardUpdater = new CardUpdater(mockGraphicCardRepository.Object);
            var expectedCard = new Domain.Entities.GraphicCard()
            {
                Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                VendorId = 1,
                MorelePrice = 1599.00M,
                XKomPrice = 1599.00M,
                LowestPrice = 1599.00M,
                LowestPriceStore = "Morele, X-Kom",
                HighestPrice = null,
                HighestPriceStore = null,
                IsPriceEqual = true
            };

            mockCardUpdater.Setup(m => m.UpdateGraphicCard(GetExampleCardFromDatabase1(), GetExampleCrawledCardWithEqualPrice()))
                .ReturnsAsync(() => expectedCard);

            //Act
            var result = await cardUpdater.UpdateGraphicCard(GetExampleCardFromDatabase1(), GetExampleCrawledCardWithEqualPrice());

            //Assert
            result.Should().BeEquivalentTo(expectedCard);
        }

        [Fact]
        public async Task UpdateGraphicCard_ForExampleCard_ReturnsCardWithHighestPrice2()
        {
            //Arrange
            var mockCardUpdater = new Mock<ICardUpdater>();
            var mockGraphicCardRepository = new Mock<IGraphicCardRepository>();
            var cardUpdater = new CardUpdater(mockGraphicCardRepository.Object);
            var expectedCard = new Domain.Entities.GraphicCard()
            {
                Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                VendorId = 1,
                MorelePrice = 1499.00M,
                XKomPrice = 1899.00M,
                LowestPrice = 1499.00M,
                LowestPriceStore = "Morele",
                HighestPrice = 1899.00M,
                HighestPriceStore = "X-Kom",
                IsPriceEqual = false
            };

            mockCardUpdater.Setup(m => m.UpdateGraphicCard(GetExampleCardFromDatabase2(), GetExampleCrawledCardWithHighestPrice2()))
                .ReturnsAsync(() => expectedCard);

            //Act
            var result = await cardUpdater.UpdateGraphicCard(GetExampleCardFromDatabase2(), GetExampleCrawledCardWithHighestPrice2());

            //Assert
            result.Should().BeEquivalentTo(expectedCard);
        }


        private static Domain.Entities.GraphicCard GetExampleCardFromDatabase1()
        {
            return new Domain.Entities.GraphicCard
            {
                Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                VendorId = 1,
                MorelePrice = 1699.00M,
                XKomPrice = null,
                LowestPrice = 1699.00M,
                LowestPriceStore = "Morele",
                HighestPrice = null,
                HighestPriceStore= null,
                IsPriceEqual = false
            };
        }

        private static Domain.Entities.GraphicCard GetExampleCardFromDatabase2()
        {
            return new Domain.Entities.GraphicCard
            {
                Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                VendorId = 1,
                MorelePrice = 1799.00M,
                XKomPrice = 1699.00M,
                LowestPrice = 1699.00M,
                LowestPriceStore = "X-Kom",
                HighestPrice = 1799.00M,
                HighestPriceStore = "Morele",
                IsPriceEqual = false
            };
        }

        private static Domain.Entities.GraphicCard GetExampleCrawledCardWithHighestPrice()
        {
            return new Domain.Entities.GraphicCard
            {
                Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                VendorId = 1,
                MorelePrice = 1599.00M,
                XKomPrice = 1799.00M,
                LowestPrice = 1599.00M,
                LowestPriceStore = "Morele",
                HighestPrice = 1799.00M,
                HighestPriceStore = "X-Kom",
                IsPriceEqual = false
            };
        }

        private static Domain.Entities.GraphicCard GetExampleCrawledCardWithHighestPrice2()
        {
            return new Domain.Entities.GraphicCard
            {
                Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                VendorId = 1,
                MorelePrice = 1499.00M,
                XKomPrice = 1899.00M,
                LowestPrice = 1499.00M,
                LowestPriceStore = "Morele",
                HighestPrice = 1899.00M,
                HighestPriceStore = "X-Kom",
                IsPriceEqual = false
            };
        }

        private static Domain.Entities.GraphicCard GetExampleCrawledCardWithoutHighestPrice()
        {
            return new Domain.Entities.GraphicCard
            {
                Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                VendorId = 1,
                MorelePrice = 1599.00M,
                XKomPrice = null,
                LowestPrice = 1599.00M,
                LowestPriceStore = "Morele",
                HighestPrice = null,
                HighestPriceStore = null,
                IsPriceEqual = false
            };
        }

        private static Domain.Entities.GraphicCard GetExampleCrawledCardWithEqualPrice()
        {
            return new Domain.Entities.GraphicCard
            {
                Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                VendorId = 1,
                MorelePrice = 1599.00M,
                XKomPrice = 1599.00M,
                LowestPrice = 1599.00M,
                LowestPriceStore= "Morele, X-Kom",
                HighestPrice = null,
                HighestPriceStore = null,
                IsPriceEqual = true
            };
        }
    }
}
