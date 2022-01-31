using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Internal
{
    using Assimalign.ComponentModel.Mapping.Abstractions;

    internal sealed class MapperProfileTargetDescriptor<TSource> :
        IMapperProfileTargetDescriptor<TSource>
    {
        public void MapTarget<TSourceMember>(System.Linq.Expressions.Expression<Func<TSource, TSourceMember>> expression)
        {
            throw new NotImplementedException();
        }

        public void MapTarget(string member)
        {
            throw new NotImplementedException();
        }

        public void Ingore()
        {
            throw new NotImplementedException();
        }
    }
}
