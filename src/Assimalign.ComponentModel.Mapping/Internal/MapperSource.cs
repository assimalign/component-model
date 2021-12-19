using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Internal
{
    using Assimalign.ComponentModel.Mapping.Abstractions;

    internal sealed class MapperSource : IMapperSource
    {
        public Type SourceType { get; }

        public MapperPaths Paths { get; }
    }
}
