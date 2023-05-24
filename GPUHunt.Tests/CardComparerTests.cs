using FluentAssertions;
using GPUHunt.Application.Interfaces;
using GPUHunt.Application.Models;
using GPUHunt.Application.Services.CardComparer;
using GPUHunt.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Tests
{
    public class CardComparerTests
    {
        [Fact]
        public async Task Compare_ForListOfGPUs_ReturnsTwoGraphicCard()
        {
            //Arrange
            var mockCardComparer = new Mock<ICardComparer>();
            var cardComparer = new CardComparer();

            mockCardComparer
                .Setup(m => m.Compare(GetFakeList()))
                .ReturnsAsync(GetExceptedList);

            //Act
            var result = await cardComparer.Compare(GetFakeList());


            //Assert
            foreach (var item in result)
            {
                var mockedGPU = GetExceptedList().FirstOrDefault(m => m.Model.ToLower() == item.Model.ToLower());
                item.Should().BeEquivalentTo(mockedGPU);
            }
        }

        [Fact]
        public async Task Compare_ForListOfGPUs_ReturnsCorrectCount()
        {
            //Arrange
            var mockCardComparer = new Mock<ICardComparer>();
            var cardComparer = new CardComparer();

            mockCardComparer
                .Setup(m => m.Compare(GetFakeList()))
                .ReturnsAsync(GetExceptedList);
            //Act
            var result = await cardComparer.Compare(GetFakeList());

            //Assert
            result.Count().Should().BeGreaterThanOrEqualTo(GetExceptedList().Count);
        }

        [Fact]
        public async Task Compare_ForGPUsWithSamePrice_ReturnsGraphicCardWithCompinedShopName()
        {
            //Arrange
            var mockCardComparer = new Mock<ICardComparer>();
            var cardComparer = new CardComparer();

            var expected = new List<Domain.Entities.GraphicCard>()
            {
                new Domain.Entities.GraphicCard()
                {
                    Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                    Vendor = "NVIDIA",
                    LowestPrice = 1699.00M,
                    LowestPriceShop = new Shop() { Name = "Morele, X-Kom" },
                    HighestPrice = null,
                    HighestPriceShop = null,
                    IsPriceEqual = true
                }
            };

            mockCardComparer
                .Setup(m => m.Compare(GetGPUsWithSamePrice()))
                .ReturnsAsync(expected);

            //Act
            var result = await cardComparer.Compare(GetGPUsWithSamePrice());

            //Assert
            foreach (var item in result)
            {
                var expectedCard = expected.FirstOrDefault(ec => ec.Model.ToLower() == item.Model.ToLower());
                item.Should().BeEquivalentTo(expectedCard);
            }
        }

        private static List<GPU> GetGPUsWithSamePrice()
        {
            var fakeList = new List<GPU>()
            {
                new GPU()
                {
                    FullName = "NVIDIA Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                    Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                    Shop = "Morele",
                    Price = 1699.00M,
                    Vendor = "NVIDIA"
                },
                new GPU()
                {
                    FullName = "NVIDIA Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                    Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                    Shop = "X-Kom",
                    Price = 1699.00M,
                    Vendor = "NVIDIA"
                }
            };

            return fakeList;
        }

        private static List<GPU> GetFakeList()
        {
            var test = new List<GPU>()
            {
                new GPU
                {
                    FullName = "NVIDIA Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                    Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                    Shop = "Morele",
                    Price = 1699.00M,
                    Vendor = "NVIDIA"
                }
                ,new GPU
                {
                    FullName = "NVIDIA Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                    Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                    Shop = "X-Kom",
                    Price = 1899.00M,
                    Vendor = "NVIDIA"
                }
                ,new GPU
                {
                    FullName = "NVIDIA MSI GeForce RTX 3060 Gaming X Trio 12GB GDDR6",
                    Model = "MSI GeForce RTX 3060 Gaming X Trio 12GB GDDR6",
                    Shop = "Morele",
                    Price = 2000.00M,
                    Vendor = "NVIDIA"
                }
            };

            return test;
        }

        private static List<Domain.Entities.GraphicCard> GetExceptedList()
        {
            var expectedList = new List<Domain.Entities.GraphicCard>()
            {
                new Domain.Entities.GraphicCard()
                {
                    Model = "Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6",
                    Vendor = "NVIDIA",
                    LowestPrice = 1699.00M,
                    LowestPriceShop = new Shop { Name = "Morele" },
                    HighestPrice = 1899.00M,
                    HighestPriceShop = new Shop { Name = "X-Kom" },
                    IsPriceEqual = false
                },
                new Domain.Entities.GraphicCard()
                {
                    Model = "MSI GeForce RTX 3060 Gaming X Trio 12GB GDDR6",
                    Vendor = "NVIDIA",
                    LowestPrice = 2000.00M,
                    LowestPriceShop = new Shop { Name = "Morele" },
                    HighestPrice = null,
                    HighestPriceShop = null,
                    IsPriceEqual = false
                },
            };

            return expectedList;
        }
    }
}
