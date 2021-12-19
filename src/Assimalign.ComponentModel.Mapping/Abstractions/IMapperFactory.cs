using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Abstractions
{
    public interface IMapperFactory
    {


        IMapper Create(IEnumerable<IMapperProfile> profiles);
    }
}
