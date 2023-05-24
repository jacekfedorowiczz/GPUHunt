using GPUHunt.Application.Models;
using GPUHunt.Domain.Entities;

namespace GPUHunt.Application.Interfaces
{
    public interface ICardComparer
    {
        Task<IEnumerable<Domain.Entities.GraphicCard>> Compare(IEnumerable<GPU> gpus);
    }
}
