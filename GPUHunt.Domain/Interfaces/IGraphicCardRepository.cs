using GPUHunt.Domain.Entities;
using GPUHunt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Domain.Interfaces
{
    public interface IGraphicCardRepository
    {
        Task<PagedResult<GraphicCard>> GetPaginatedCards(GraphicCardQuery query);
        Task<IEnumerable<GraphicCard>> GetAll();
        Task Crawl(IEnumerable<GraphicCard> graphicCards);
        Task Update(GraphicCard graphicCard);
        Task Delete(int id);
        Task<bool> IsDatabaseNotEmpty();
    }
}
