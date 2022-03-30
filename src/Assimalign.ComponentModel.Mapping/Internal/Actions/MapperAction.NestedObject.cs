using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.ComponentModel.Mapping.Internal;

using Assimalign.ComponentModel.Mapping.Properties;
using Assimalign.ComponentModel.Mapping.Internal.Exceptions;

internal sealed class MapperActionNestedObject<TTarget, TTargetMember, TSource, TSourceMember> : IMapperAction
{

    public MapperActionNestedObject(Expression<Func<TTarget, TTargetMember>> target, Expression<Func<TSource, TSourceMember>> source)
    {
        if (target.Body is not MemberExpression member)
        {
            throw new ArgumentException($"The target expression body: '{target}' must be a MemberExpression.");
        }
        if (member.Member.DeclaringType != typeof(TTarget))
        {
            throw new Exception(string.Format(Resources.MapperExceptionInvalidChaining, target, typeof(TTarget).Name));
        }

        SourceExpression = source;
        SourceGetter = source.Compile();
        TargetExpression = target;
        TargetMember = member.Member;
        TargetGetter = target.Compile();
    }

    public int Id => this.TargetType.GetHashCode() + TargetMember.GetHashCode();
    public Type TargetType => typeof(TTarget);
    public MemberInfo TargetMember { get; }
    public Func<TTarget, TTargetMember> TargetGetter { get; }
    public Expression<Func<TTarget, TTargetMember>> TargetExpression { get; }

    public Type SourceType => typeof(TSource);
    public Func<TSource, TSourceMember> SourceGetter { get; }
    public Expression<Func<TSource, TSourceMember>> SourceExpression { get; }
    // To prevent searching a profile in the Mapper options lets just store the reference in a property.
    public IMapperProfile Profile { get; init; }

    public void Invoke(MapperContext context)
    {
        if (context.Source is not TSource source)
        {
            throw new MapperInvalidContextException(context.Source.GetType(), context.Target.GetType(), typeof(TSource), typeof(TTarget));
        }
        if (context.Target is not TTarget target)
        {
            throw new MapperInvalidContextException(context.Source.GetType(), context.Target.GetType(), typeof(TSource), typeof(TTarget));
        }

        var sourceValue = GetValue(source);

        if (sourceValue is not null)
        {
            var nestedContext = new MapperContext(targetValue, sourceValue);

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
            return SourceGetter.Invoke(source);
        }
        catch (Exception exception) when (exception is NullReferenceException)
        {
            return default;
        }
    }
    private void SetValue(object instance, object value)
    {
        if (TargetMember is PropertyInfo property)
        {
            property.SetValue(instance, value);
        }
        else if (TargetMember is FieldInfo field)
        {
            field.SetValue(instance, value);
        }
    }
}