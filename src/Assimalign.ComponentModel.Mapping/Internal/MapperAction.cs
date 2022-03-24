using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.ComponentModel.Mapping.Internal;

internal sealed class MapperAction<TSource, TSourceMember, TTarget, TTargetMember> : IMapperAction
{
	private readonly MemberInfo targetMember;
	private readonly Func<TSource, TSourceMember> sourceMember;

	public MapperAction(
		Expression<Func<TSource, TSourceMember>> source,
		Expression<Func<TTarget, TTargetMember>> target)
	{
		if (target.Body is MemberExpression member)
		{
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

	public string TargetItem { get; }


	public void Invoke(MapperContext context)
	{
		if (context.Source is TSource source && context.Target is TTarget target)
		{
			var sourceValue = sourceMember.Invoke(source);

			SetValue(target, sourceValue);
		}
		else
		{
			throw new ArgumentException("");
		}
	}

	private void SetValue(object instance, object value)
	{
		if (targetMember is PropertyInfo property)
		{
			property.SetValue(instance, value);
		}
		if (targetMember is FieldInfo field)
		{
			field.SetValue(instance, value);
		}
	}

	public bool Equals(IMapperAction item)
	{
		return this.TargetType == item.TargetType &&
			this.TargetItem == item.TargetItem;
	}
	public override bool Equals(object? obj)
	{
		if (obj is IMapperAction item)
		{
			return this.Equals(item);
		}
		else
		{
			return false;
		}
	}
	public override int GetHashCode()
	{
		return HashCode.Combine(TargetType, TargetItem);
	}

    public bool Equals(IMapperAction x, IMapperAction y)
    {
        throw new NotImplementedException();
    }

    public int GetHashCode([DisallowNull] IMapperAction obj)
    {
        throw new NotImplementedException();
    }
}
