using System;

namespace Assimalign.ComponentModel.Mapping.Internal;

internal sealed class MapperProfileDefault<TSource, TTarget> : MapperProfile<TSource, TTarget>
{
    private readonly Action<IMapperProfileDescriptor<TSource, TTarget>> configure;

    public MapperProfileDefault(Action<IMapperProfileDescriptor<TSource, TTarget>> configure)
    {
        this.configure = configure;
    }


    public override void Configure(IMapperProfileDescriptor<TSource, TTarget> descriptor)
    {
        configure.Invoke(descriptor);
    }
}
