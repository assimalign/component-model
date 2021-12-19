using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Internal
{
    using Assimalign.ComponentModel.Mapping.Abstractions;

    internal sealed class MapperTarget : IMapperTarget
    {
        public Type TargetType { get; }

        public MapperPaths Paths { get; }
    }
}
