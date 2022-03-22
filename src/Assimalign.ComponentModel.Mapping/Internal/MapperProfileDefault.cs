
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping;

using Assimalign.ComponentModel.Mapping.Abstractions;

internal sealed class MapperProfileDefault<TContract, TBinding> : MapperProfile<TContract, TBinding>
{

    public override void Configure(IMapperProfileDescriptor<TContract, TBinding> descriptor)
    {
        throw new NotImplementedException();
    }
}
