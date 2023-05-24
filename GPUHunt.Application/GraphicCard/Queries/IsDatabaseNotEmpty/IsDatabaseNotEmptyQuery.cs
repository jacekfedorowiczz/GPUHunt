using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.GraphicCard.Queries.IsDatabaseNotEmpty
{
    public class IsDatabaseNotEmptyQuery : IRequest<bool>
    {
    }
}
