using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.ComponentModel.Mapping.Internal;

using Assimalign.ComponentModel.Mapping.Properties;

public class MapperActionNestedObject<TTarget, TTargetMember, TSource, TSourceMember> : IMapperAction
    where TTargetMember : new()
    where TSourceMember : new()
{
    private readonly MemberInfo targetMember;
    private readonly Func<TSource, TSourceMember> sourceMember;

    public MapperActionNestedObject(Expression<Func<TTarget, TTargetMember>> target, Expression<Func<TSource, TSourceMember>> source)
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
            var targetValue = new TTargetMember();
            var sourceValue = GetValue(source);

            if (sourceValue is null)
            {
                return;
            }
            
            var nestedContext = new MapperContext(sourceValue, targetValue);

            foreach (var action in Profile.MapActions)
            {
                action.Invoke(nestedContext);
            }

            SetValue(target, targetValue);
        }
    }

    private TSourceMember GetValue(TSource source)
    {
        try
        {
            return sourceMember.Invoke(source);
        }
        catch (Exception exception) when (exception is NullReferenceException)
        {
            return default;
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