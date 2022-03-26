using System;

namespace Assimalign.ComponentModel.Mapping.Internal;

internal sealed class MapperAction<TTarget, TSource> : IMapperAction
{
	private readonly Action<TTarget, TSource> action;
    
	public MapperAction(Action<TTarget, TSource> action)
    {
		this.action = action;
    }

    public void Invoke(MapperContext context)
    {
        if (context.Source is TSource source && context.Target is TTarget target)
        {
			action.Invoke(target, source);
        }
		else
        {
			throw new Exception("Invalid Context");
        }
    }
}