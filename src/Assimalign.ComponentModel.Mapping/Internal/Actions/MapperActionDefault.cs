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
