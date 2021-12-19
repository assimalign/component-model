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
    public interface IMapperTarget
    {
        /// <summary>
        /// 
        /// </summary>
        MapperPaths Paths { get; }
        /// <summary>
        /// 
        /// </summary>
        Type TargetType { get; }
    }
}
