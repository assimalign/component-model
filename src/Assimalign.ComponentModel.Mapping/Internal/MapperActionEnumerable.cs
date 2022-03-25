using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.ComponentModel.Mapping.Internal;

internal sealed class MapperActionEnumerable<TSource, TSourceMember, TTarget, TTargetMember> : IMapperAction
	where TTargetMember : new()
{
	private readonly MemberInfo targetMember;
	private readonly Func<TSource, IEnumerable<TSourceMember>> sourceMember;

	public MapperActionEnumerable(Expression<Func<TSource, IEnumerable<TSourceMember>>> source, Expression<Func<TTarget, IEnumerable<TTargetMember>>> target)
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


	public IMapperProfile Profile { get; set; }


	public void Invoke(MapperContext context)
    {
        if (context.Source is TSource source && context.Target is TTarget target)
        {
			var items = new List<object>();
			var sourceValues = GetValue(source);

			if (sourceValues is null)
            {
				return;
            }

			foreach (var sourceValue in sourceValues)
            {
				var targetValue = new TTargetMember();
				var nestedContext = new MapperContext(sourceValue, targetValue);

				foreach (var action in Profile.MapActions)
                {
					action.Invoke(nestedContext);
                }

				items.Add(targetValue);
            }

			SetValue(target, items.Cast<TTargetMember>().AsEnumerable());
        }
    }

	private IEnumerable<TSourceMember> GetValue(TSource source)
	{
		try
		{
			return sourceMember.Invoke(source);
		}
		catch (Exception exception) when (exception is NullReferenceException)
		{
			return null;
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


}