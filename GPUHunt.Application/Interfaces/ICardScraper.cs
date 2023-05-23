using System.Threading.Tasks;

namespace GPUHunt.Application.Interfaces
{
    public interface ICardScraper
    {
        Task<string> Scrap();
    }
}
