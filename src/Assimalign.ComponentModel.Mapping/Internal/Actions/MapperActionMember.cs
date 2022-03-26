using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.ComponentModel.Mapping.Internal;

using Assimalign.ComponentModel.Mapping.Properties;
using Assimalign.ComponentModel.Mapping.Internal.Exceptions;

/* 
 * This Mapper Action is for member to member mapping
 */
internal sealed class MapperActionMember<TSource, TSourceMember, TTarget, TTargetMember> : IMapperAction
{
    private readonly MemberInfo targetMember;
    private readonly Func<TSource, TSourceMember> sourceMember;

    public MapperActionMember(Expression<Func<TSource, TSourceMember>> source, Expression<Func<TTarget, TTargetMember>> target)
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
            throw new MapperInvalidContextException(context.Source.GetType(), context.Target.GetType(), typeof(TSource), typeof(TTarget));
        }
    }
    private object GetValue(TSource source)
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
    private void SetValue(object targetInstance, object targetValue)
    {
        switch (targetMember)
        {
            case PropertyInfo property:
                {
                    property.SetValue(targetInstance, targetValue);
                    break;
                }
            case FieldInfo field:
                {
                    field.SetValue(targetInstance, targetValue);
                    break;
                }
        }
    }

    public override bool Equals(object obj) => obj is IMapperAction item ? this.Equals(item) : false;
    public override int GetHashCode() => HashCode.Combine(TargetType, TargetMember);
}
