using System;

namespace Assimalign.ComponentModel.Mapping.Internal;

internal sealed class MapperActionDefault : IMapperAction
{
    private readonly Action<IMapperContext> configure;

    public MapperActionDefault(Action<IMapperContext> configure)
    {
        this.configure = configure;
    }

    public int Id => this.GetHashCode();

    public void Invoke(IMapperContext context)
    {
        configure.Invoke(context);
    }
}

internal sealed class MapperActionDefault<TTarget, TSource> : IMapperAction
{
    private readonly Action<TTarget, TSource> configure;

    public MapperActionDefault(Action<TTarget, TSource> configure)
    {
        this.configure = configure;
    }

    public int Id => this.GetHashCode();

    public void Invoke(IMapperContext context)
    {
        if (context.Target is TTarget target && context.Source is TSource source)
        {
            configure.Invoke(target, source);
        }
    }
}
