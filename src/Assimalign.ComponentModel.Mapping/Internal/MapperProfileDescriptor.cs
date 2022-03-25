using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Internal;

internal sealed class MapperProfileDescriptor<TSource, TTarget> : IMapperProfileDescriptor<TSource, TTarget>
{
    public MapperProfileDescriptor()
    {
        this.BeforeActions = new List<IMapperAction>();
        this.MapActions =  new List<IMapperAction>();
        this.AfterActions = new List<IMapperAction>();
    }


    // Passing all added profiles from options as reference to be able to register nested profiles
    public IList<IMapperAction> BeforeActions { get; set; }
    public IList<IMapperAction> MapActions { get; set;  }
    public IList<IMapperAction> AfterActions { get; set;  }
    public IList<IMapperProfile> Profiles { get; set; }

    public IMapperProfileDescriptor AddMapAction(IMapperAction action) 
    {
        MapActions.Add(action);
        return this;
    }

    public IMapperProfileDescriptor<TSource, TTarget> AddProfile<TSourceMember, TTargetMember>(
        Expression<Func<TSource, IEnumerable<TSourceMember>>> source,
        Expression<Func<TTarget, IEnumerable<TTargetMember>>> target,
        Action<IMapperProfileDescriptor<TSourceMember, TTargetMember>> configure)
            where TSourceMember : class
            where TTargetMember : class, new()
    {
        var profile = new MapperProfileDefault<TSourceMember, TTargetMember>(configure);

        if (this.Profiles.Contains(profile))
        {
            throw new Exception("");
        }

        IMapperProfileDescriptor descriptor = new MapperProfileDescriptor<TSourceMember, TTargetMember>()
        {
            Profiles = this.Profiles
        };

        profile.Configure(descriptor);

        this.Profiles.Add(profile);

        var action = new MapperActionEnumerable<TSource, TSourceMember, TTarget, TTargetMember>(source, target)
        {
            Profile = profile
        };

        this.AddMapAction(action);

        return this;
    }

    public IMapperProfileDescriptor<TSource, TTarget> AddProfile<TSourceMember, TTargetMember>(
        Expression<Func<TSource, TSourceMember>> source,
        Expression<Func<TTarget, TTargetMember>> target,
        Action<IMapperProfileDescriptor<TSourceMember, TTargetMember>> configure)
            where TSourceMember : class
            where TTargetMember : class, new()
    {
        var profile = new MapperProfileDefault<TSourceMember, TTargetMember>(configure);

        //if (this.Profiles.Contains(profile))
        //{
        //    throw new Exception("");
        //}

        var descriptor = new MapperProfileDescriptor<TSourceMember, TTargetMember>()
        {
            Profiles = this.Profiles
        };

        profile.Configure(descriptor);

        this.Profiles.Add(profile);

        return this;
    }

    public IMapperProfileDescriptor<TSource, TTarget> AfterMap(Action<TSource, TTarget> action)
    {
        this.AfterActions.Add(new MapperAction<TSource, TTarget>(action));
        return this;
    }

    public IMapperProfileDescriptor<TSource, TTarget> BeforeMap(Action<TSource, TTarget> action)
    {
        this.BeforeActions.Add(new MapperAction<TSource, TTarget>(action));
        return this;
    }

    public void DisableDefaultMapping()
    {
        throw new NotImplementedException();
    }


    public IMapperProfileDescriptor<TSource, TTarget> MapMembers(string source, string target)
    {
        var sourceParameter = Expression.Parameter(typeof(TSource));
        var targetParameter = Expression.Parameter(typeof(TTarget));
        var sourceParameterMember = Expression.PropertyOrField(sourceParameter, source);
        var targetParameterMember = Expression.PropertyOrField(targetParameter, target);

        var sourceLambda = Expression.Lambda(sourceParameterMember, sourceParameter);
        var targetLambda = Expression.Lambda(targetParameterMember, targetParameter);

        var mapperActionType = typeof(MapperAction<,,,>).MakeGenericType(
            typeof(TSource), 
            sourceParameterMember.Type, 
            typeof(TTarget), 
            targetParameterMember.Type);

        var mapperAction = Activator.CreateInstance(mapperActionType, sourceLambda, targetLambda) as IMapperAction;
        this.AddMapAction(mapperAction);

        return this;
    }
    public IMapperProfileDescriptor<TSource, TTarget> MapMembers<TSourceMember, TTargetMember>(
        Expression<Func<TSource, TSourceMember>> source, 
        Expression<Func<TTarget, TTargetMember>> target) 
            where TSourceMember : TTargetMember
    {
        var action = new MapperAction<TSource, TSourceMember, TTarget, TTargetMember>(source, target);

        //if (Current.MapActions.Contains(action))
        //{
        //    throw new Exception("There is a duplicate action");
        //}

        this.AddMapAction(action);

        return this;
    }
}
