using GPUHunt.Domain.Entities;
using GPUHunt.Domain.Interfaces;
using GPUHunt.Domain.Models;
using GPUHunt.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Infrastructure.Repositories
{
    public class GraphicCardRepository : IGraphicCardRepository
    {
        private readonly GPUHuntDbContext _dbContext;

        public GraphicCardRepository(GPUHuntDbContext dbContext)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<PagedResult<GraphicCard>> GetPaginatedCards(GraphicCardQuery query)
        {
            var baseQuery = _dbContext
                                .GraphicCards
                                .Where(g => query.SearchPhrase == null || g.Model.ToLower().Contains(query.SearchPhrase.ToLower()))
                                .AsNoTracking()
                                .AsQueryable();

            var columnSelectors = new Dictionary<string, Expression<Func<GraphicCard, object>>>
            {
                { nameof(GraphicCard.Model), g => g.Model },
                { nameof(GraphicCard.Vendor), g => g.Vendor },
                { nameof(GraphicCard.LowestPrice), g => g.LowestPrice },
                { nameof(GraphicCard.HighestPrice), g => g.HighestPrice }
            };

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var selectedColumn = columnSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var graphicCards = await baseQuery
                    .Skip(query.PageSize * (query.PageNumber - 1))
                    .Take(query.PageSize)
                    .ToListAsync();

            var totalCount = baseQuery.Count();

            return new PagedResult<GraphicCard>(graphicCards, totalCount, query.PageSize, query.PageNumber);
        }

        public async Task<IEnumerable<GraphicCard>> GetAll() => await _dbContext.GraphicCards.AsNoTracking().ToListAsync();

        public async Task Commit() => await _dbContext.SaveChangesAsync();

        public async Task Crawl(IEnumerable<GraphicCard> graphicCards)
        {
            _dbContext.GraphicCards.AddRange(graphicCards);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(GraphicCard graphicCard)
        {
            var cardFromDatabase = await _dbContext.GraphicCards.FirstOrDefaultAsync(cd => cd.Model.ToLower() == graphicCard.Model.ToLower());

            if (cardFromDatabase == null)
            {
                return;
            }

            cardFromDatabase.LowestPrice = graphicCard.LowestPrice;
            cardFromDatabase.LowestPriceShop = graphicCard.LowestPriceShop;
            cardFromDatabase.HighestPrice = graphicCard.HighestPrice;
            cardFromDatabase.HighestPriceShop = graphicCard.HighestPriceShop;
            cardFromDatabase.IsPriceEqual = graphicCard.IsPriceEqual;

            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            try
            {
                var gpu = await _dbContext.GraphicCards.FirstOrDefaultAsync(x => x.Id == id);
                _dbContext.GraphicCards.Remove(gpu);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong.");
            }
        }

        public async Task<bool> IsDatabaseNotEmpty()
        {
            try
            {
                if (await _dbContext.Database.CanConnectAsync())
                {
                    return _dbContext.GraphicCards.Any();
                }
                else
                {
                    throw new Exception("Cannot connect to the database.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
