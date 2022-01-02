using Assimalign.ComponentModel.Mapping.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping
{
    internal sealed class MapperProfileDefault<TSource, TTarget> : MapperProfile<TSource, TTarget>
    {

        public override void Configure(IMapperProfileDescriptor<TSource, TTarget> descriptor)
        {
            throw new NotImplementedException();
        }
    }
}
