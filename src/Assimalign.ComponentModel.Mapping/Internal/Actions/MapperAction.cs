using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.ComponentModel.Mapping.Internal;

internal sealed class MapperAction<TSource, TTarget> : IMapperAction
{
	private readonly Action<TSource, TTarget> action;
    
	public MapperAction(Action<TSource, TTarget> action)
    {
		this.action = action;
    }

    public void Invoke(MapperContext context)
    {
        if (context.Source is TSource source && context.Target is TTarget target)
        {
			action.Invoke(source, target);
        }
		else
        {
			throw new Exception("Invalid Context");
        }
    }
}