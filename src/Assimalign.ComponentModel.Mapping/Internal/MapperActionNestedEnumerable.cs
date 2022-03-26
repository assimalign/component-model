using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Mapping.Internal;

using Assimalign.ComponentModel.Mapping.Properties;

internal sealed class MapperActionNestedEnumerable<TTarget, TTargetMember, TSource, TSourceMember> : IMapperAction
    where TSourceMember : new()
    where TTargetMember : new()
{
    private readonly MemberInfo targetMember;
    private readonly Func<TSource, IEnumerable<TSourceMember>> sourceMember;

    public MapperActionNestedEnumerable(Expression<Func<TTarget, IEnumerable<TTargetMember>>> target, Expression<Func<TSource, IEnumerable<TSourceMember>>> source)
    {
        if (target.Body is MemberExpression member)
        {
            if (member.Member.DeclaringType != typeof(TTarget))
            {
                throw new Exception(string.Format(Resources.MapperExceptionInvalidChaining, target, typeof(TTarget).Name));
            }

            targetMember = member.Member;
            sourceMember = source.Compile();
        }
        else
        {
            throw new ArgumentException($"The target expression body: '{target}' must be a MemberExpression.");
        }
    }

    // To prevent searching a profile in the Mapper options lets just store the reference in a property.
    public IMapperProfile Profile { get; set; }

    public void Invoke(MapperContext context)
    {
        if (context.Source is TSource source && context.Target is TTarget target)
        {
            var items = new List<object>();
            var sourceValues = GetValue(source);

            if (sourceValues is null) // No need to try mapping target if there is no data to map
            {
                return;
            }

            foreach (var sourceValue in sourceValues)
            {
                var targetValue = new TTargetMember();
                var nestedContext = new MapperContext(targetValue, sourceValue);

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