using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Domain.Entities
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Domain.Entities.GraphicCard> GraphicCards { get; set; }
    }
}
