﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping;

public interface IMapperStrategy
{
    IMapper Create(IEnumerable<IMapperProfile> profiles);
}
