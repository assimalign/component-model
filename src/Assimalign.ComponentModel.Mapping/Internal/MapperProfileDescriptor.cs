using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Internal;

internal sealed class MapperProfileDescriptor<TSource, TTarget> : IMapperProfileDescriptor<TSource, TTarget>
{
    // Passing all added profiles from options as reference to be able to register nested profiles
    public IList<IMapperProfile> Profiles { get; set; } 

    public IMapperProfile Current { get; set; }

    public IMapperProfileDescriptor AddMapperAction(IMapperAction item)
    {
        throw new NotImplementedException();
    }

    public IMapperProfileDescriptor<TSource, TTarget> AddProfile<TSourceMember, TTargetMember>(
        Expression<Func<TSource, IEnumerable<TSourceMember>>> source, 
        Expression<Func<TTarget, IEnumerable<TTargetMember>>> target, 
        Action<IMapperProfileDescriptor<TSourceMember, TTargetMember>> configure)
            where TSourceMember : class
            where TTargetMember : class
    {
        var profile = new MapperProfileDefault<TSourceMember, TTargetMember>(configure);

        if (this.Profiles.Contains(profile))
        {
            throw new Exception("");
        }

        var descriptor = new MapperProfileDescriptor<TSourceMember, TTargetMember>()
        {
            Profiles = this.Profiles,
            Current = profile
        };

        profile.Configure(descriptor);

        this.Profiles.Add(profile);

        return this;
    }

    public IMapperProfileDescriptor<TSource, TTarget> AddProfile<TSourceMember, TTargetMember>(
        Expression<Func<TSource, TSourceMember>> source, 
        Expression<Func<TTarget, TTargetMember>> target,
        Action<IMapperProfileDescriptor<TSourceMember, TTargetMember>> configure)
            where TSourceMember : class
            where TTargetMember : class
    {
        var profile = new MapperProfileDefault<TSourceMember, TTargetMember>(configure);

        if (this.Profiles.Contains(profile))
        {
            throw new Exception("");
        }

        var descriptor = new MapperProfileDescriptor<TSourceMember, TTargetMember>()
        {
            Profiles = this.Profiles,
            Current = profile
        };

        profile.Configure(descriptor);

        this.Profiles.Add(profile);

        return this;
    }

    public IMapperProfileDescriptor<TSource, TTarget> AfterMap(Action<TSource, TTarget> action)
    {
        
        throw new NotImplementedException();
    }

    public IMapperProfileDescriptor<TSource, TTarget> BeforeMap(Action<TSource, TTarget> action)
    {
        throw new NotImplementedException();
    }

    public void DisableDefaultMapping()
    {
        throw new NotImplementedException();
    }

    public IMapperProfileTargetDescriptor<TSource, TTarget> ForSource(string member)
    {
        throw new NotImplementedException();
    }

    public IMapperProfileTargetDescriptor<TSource, TTarget> ForSource<TSourceMember>(System.Linq.Expressions.Expression<Func<TSource, TSourceMember>> expression)
    {
        throw new NotImplementedException();
    }

    public IMapperProfileSourceDescriptor<TSource, TTarget> ForTarget(string member)
    {
        throw new NotImplementedException();
    }

    public IMapperProfileSourceDescriptor<TSource, TTarget> ForTarget<TTargetMember>(System.Linq.Expressions.Expression<Func<TTarget, TTargetMember>> expression)
    {
        throw new NotImplementedException();
    }

    public IMapperProfileDescriptor<TSource, TTarget> MapMembers(string source, string target)
    {
        throw new NotImplementedException();
    }

    public IMapperProfileDescriptor<TSource, TTarget> MapMembers<TSourceMember, TTargetMember>(System.Linq.Expressions.Expression<Func<TSource, TSourceMember>> source, System.Linq.Expressions.Expression<Func<TTarget, TTargetMember>> target) where TSourceMember : TTargetMember
    {
        throw new NotImplementedException();
    }
}
