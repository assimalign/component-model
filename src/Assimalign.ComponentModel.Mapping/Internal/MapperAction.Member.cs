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
internal sealed class MapperActionMember<TTarget, TTargetMember, TSource, TSourceMember> : IMapperAction
    where TSourceMember : TTargetMember
{
    public MapperActionMember(Expression<Func<TTarget, TTargetMember>> target, Expression<Func<TSource, TSourceMember>> source)
    {
        if (target.Body is MemberExpression member)
        {
            // Ensure that the member is of type TTarget (Target Members cannot be nested.)
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
        else
        {
            throw new ArgumentException($"The target expression body: '{target}' must be a MemberExpression.");
        }
    }

    public int Id  => this.TargetType.GetHashCode() + TargetMember.GetHashCode();
    public Type TargetType => typeof(TTarget);
    public MemberInfo TargetMember { get; }
    public Func<TTarget, TTargetMember> TargetGetter { get; }
    public Expression<Func<TTarget, TTargetMember>> TargetExpression { get; }

    public Type SourceType => typeof(TSource);
    public Func<TSource, TSourceMember> SourceGetter { get; }
    public Expression<Func<TSource, TSourceMember>> SourceExpression { get; }

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
            return SourceGetter.Invoke(source);
        }
        // Let's catch the exception for Null References only. This occurs when the Source Member Expression is chained and possibly null.
        catch (Exception exception) when (exception is NullReferenceException)
        {
            return default(TSourceMember);
        }
    }
    private void SetValue(object targetInstance, object targetValue)
    {
        switch (TargetMember)
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
            default:
                {
                    // This should never hit, but added just encase
                    throw new NotSupportedException($"The Target Member  of expression '{TargetExpression}' is not supported. Unknown System.Reflection.MemberInfo.");
                }
        }
    }

    public override bool Equals(object instance) => instance is IMapperAction action ? action.Id == this.Id : false;

    public override int GetHashCode() => HashCode.Combine(TargetType, TargetMember);
}
