using System;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Mapping;


///<summary>
///
///</summary>
public interface IMapperFactory
{

    ///<summary>
    ///
    ///</summary>
    IMapper Create(IEnumerable<IMapperProfile> profiles);
}