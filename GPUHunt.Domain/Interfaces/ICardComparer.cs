using GPUHunt.Domain.Entities;
using GPUHunt.Domain.Models;

namespace GPUHunt.Domain.Interfaces
{
    public interface ICardComparer
    {
        Task<IEnumerable<GraphicCard>> Compare(IEnumerable<GPU> gpus);
    }
}
