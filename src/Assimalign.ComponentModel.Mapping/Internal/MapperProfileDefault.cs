using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Mapping.Internal;

internal sealed class MapperProfileDefault<TSource, TTarget> : MapperProfile<TSource, TTarget>
{
    private readonly Action<IMapperActionDescriptor<TSource, TTarget>> configure;

    public MapperProfileDefault(Action<IMapperActionDescriptor<TSource, TTarget>> configure)
    {
        this.configure = configure;
    }


    public override void Configure(IMapperActionDescriptor<TSource, TTarget> descriptor)
    {
        configure.Invoke(descriptor);
    }
}
