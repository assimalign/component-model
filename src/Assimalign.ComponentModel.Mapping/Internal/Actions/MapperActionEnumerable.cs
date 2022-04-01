using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Mapping.Internal;

using Assimalign.ComponentModel.Mapping.Properties;
using Assimalign.ComponentModel.Mapping.Internal.Exceptions;

internal sealed class MapperActionEnumerable<TTarget, TTargetMember, TSource, TSourceMember> : IMapperAction
    where TSourceMember : new()
    where TTargetMember : new()
{
    private Func<IEnumerable<TTargetMember>, IEnumerable<TTargetMember>> convert;

    public MapperActionEnumerable(Expression<Func<TTarget, IEnumerable<TTargetMember>>> target, Expression<Func<TSource, IEnumerable<TSourceMember>>> source)
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

        SetEnumerableConverter();
    }

    private void SetEnumerableConverter()
    {
        if (typeof(List<TTargetMember>).IsAssignableTo(TargetExpression.ReturnType))
        {
            convert = enumerable => enumerable.ToList();
        }
        else if (typeof(TSourceMember[]).IsAssignableTo(TargetExpression.ReturnType))
        {
            convert = enumerable => enumerable.ToArray();
        }
        else if (typeof(IEnumerable<TSourceMember>).IsAssignableTo(TargetExpression.ReturnType))
        {
            convert = enumerable => enumerable.AsEnumerable();
        }
        else
        {
            convert = enumerable => enumerable;
        }
    }

    public int Id => this.TargetType.GetHashCode() + TargetMember.GetHashCode();
    public Type TargetType => typeof(TTarget);
    public MemberInfo TargetMember { get; }
    public Func<TTarget, IEnumerable<TTargetMember>> TargetGetter { get; }
    public Expression<Func<TTarget, IEnumerable<TTargetMember>>> TargetExpression { get; }

    public Type SourceType => typeof(TSource);
    public Func<TSource, IEnumerable<TSourceMember>> SourceGetter { get; }
    public Expression<Func<TSource, IEnumerable<TSourceMember>>> SourceExpression { get; }

    // To prevent searching a profile in the Mapper options lets just store the reference in a property.
    public IMapperProfile Profile { get; init; }

    public void Invoke(IMapperContext context)
    {
        if (context.Source is not TSource source)
        {
            throw new MapperInvalidContextException(context.Source.GetType(), context.Target.GetType(), typeof(TSource), typeof(TTarget));
        }
        if (context.Target is not TTarget target)
        {
            throw new MapperInvalidContextException(context.Source.GetType(), context.Target.GetType(), typeof(TSource), typeof(TTarget));
        }

        var sourceValues = GetValue(source);

        if (sourceValues is not null) // No need to try mapping target if there is no data to map
        {
            var items = new List<TTargetMember>();

            foreach (var sourceValue in sourceValues)
            {
                TTargetMember targetValue = default;
                var nestedContext = new MapperContext(targetValue, sourceValue);

                foreach (var action in Profile.MapActions)
                {
                    action.Invoke(nestedContext);
                }

                items.Add(targetValue);
            }

            SetValue(target, convert.Invoke(items));
        }
    }

    private IEnumerable<TSourceMember> GetValue(TSource source)
    {
        try
        {
            return SourceGetter.Invoke(source);
        }
        catch (Exception exception) when (exception is NullReferenceException)
        {
            return null;
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