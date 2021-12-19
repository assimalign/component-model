using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMapperSource
    {
        /// <summary>
        /// 
        /// </summary>
        MapperPaths Paths { get; }
        /// <summary>
        /// 
        /// </summary>
        Type SourceType { get; }
    }
}
