using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Internal;

using Assimalign.ComponentModel.Mapping.Properties;

internal sealed class MapperActionList<TTarget, TTargetMember, TSource, TSourceMember> : IMapperAction
    where TSourceMember : new()
    where TTargetMember : new()
{
    public MapperActionList(Expression<Func<TTarget, IList<TTargetMember>>> target, Expression<Func<TSource, IEnumerable<TSourceMember>>> source)
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
    public Func<TTarget, IList<TTargetMember>> TargetGetter { get; }
    public Expression<Func<TTarget, IList<TTargetMember>>> TargetExpression { get; }
    public Type SourceType => typeof(TSource);
    public Func<TSource, IEnumerable<TSourceMember>> SourceGetter { get; }
    public Expression<Func<TSource, IEnumerable<TSourceMember>>> SourceExpression { get; }

    public void Invoke(MapperContext context)
    {
        throw new NotImplementedException();
    }
}
