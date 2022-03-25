using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.ComponentModel.Mapping.Internal;

/* This Mapper Action 
 */
internal sealed class MapperAction<TSource, TSourceMember, TTarget, TTargetMember> : IMapperAction
{
	private readonly MemberInfo targetMember;
	private readonly Func<TSource, TSourceMember> sourceMember;

	public MapperAction(Expression<Func<TSource, TSourceMember>> source, Expression<Func<TTarget, TTargetMember>> target)
	{
		if (target.Body is MemberExpression member)
		{
			if (member.Member.DeclaringType != typeof(TTarget))
            {
				throw new Exception("Chained mapping is not allowed for target. All Target values must be of the parent type: 'target'");
            }

			targetMember = member.Member;
			sourceMember = source.Compile();
		}
		else
		{
			throw new ArgumentException($"The target expression body: '{target}' must be a MemberExpression.");
		}
	}

	public Type SourceType => typeof(TSource);
	public Type TargetType => typeof(TTarget);
	public MemberInfo TargetMember => targetMember;
	public void Invoke(MapperContext context)
	{
		if (context.Source is TSource source && context.Target is TTarget target)
		{
			SetValue(target, GetValue(source));
		}
		else
		{
			throw new ArgumentException("");
		}
	}
	private object GetValue(TSource source)
    {
        try
        {
			return sourceMember.Invoke(source);
		}
        catch(Exception exception) when (exception is NullReferenceException)
        {
			return null;
        }
    }
	private void SetValue(object targetInstance, object targetVAlue)
	{
		if (targetMember is PropertyInfo property)
		{
			property.SetValue(targetInstance, targetVAlue);
		}
		if (targetMember is FieldInfo field)
		{
			field.SetValue(targetInstance, targetVAlue);
		}
	}

	public override bool Equals(object obj) => obj is IMapperAction item ? this.Equals(item) : false;
	public override int GetHashCode() => HashCode.Combine(TargetType, TargetMember);
}

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