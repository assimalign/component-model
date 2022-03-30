using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping;

public abstract class MapperFactory : IMapperFactory
{
    public abstract void Configure(string strategyName, Action<IMapperFactoryBuilder> builder);


    public IMapper Create(string strategyName)
    {
        throw new NotImplementedException();
    }
}
