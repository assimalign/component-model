using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Internal;

internal sealed class MapperActionDescriptor<TTarget, TSource> : IMapperActionDescriptor<TTarget, TSource>
{
    public MapperActionDescriptor()
    {
        this.PreActions = new List<IMapperAction>();
        this.MapActions =  new List<IMapperAction>();
        this.PostActions = new List<IMapperAction>();
    }
    
    public IList<IMapperAction> PreActions { get; }
    public IList<IMapperAction> MapActions { get; }
    public IList<IMapperAction> PostActions { get; }
    public IList<IMapperProfile> Profiles { get; set; } // Passing all added profiles from options as reference to be able to register nested profiles

    public IMapperActionDescriptor MapAction(IMapperAction action) 
    {
        MapActions.Add(action);
        return this;
    }

    public IMapperActionDescriptor<TTarget, TSource> MapProfile<TTargetMember, TSourceMember>(Expression<Func<TTarget, IEnumerable<TTargetMember>>> target, Expression<Func<TSource, IEnumerable<TSourceMember>>> source, Action<IMapperActionDescriptor<TTargetMember, TSourceMember>> configure)
        where TTargetMember : class, new()
        where TSourceMember : class, new()
    {
        var profile = new MapperProfileDefault<TTargetMember, TSourceMember>(configure);

        if (this.Profiles.Contains(profile))
        {
            throw new Exception("");
        }

        IMapperActionDescriptor descriptor = new MapperActionDescriptor<TTargetMember, TSourceMember>()
        {
            Profiles = this.Profiles
        };

        profile.Configure(descriptor);

        this.Profiles.Add(profile);

        var action = new MapperActionNestedEnumerable<TTarget, TTargetMember, TSource, TSourceMember>(target, source)
        {
            Profile = profile
        };

        this.MapAction(action);

        return this;
    }

    public IMapperActionDescriptor<TTarget, TSource> MapProfile<TTargetMember, TSourceMember>(Expression<Func<TTarget, TTargetMember>> target, Expression<Func<TSource, TSourceMember>> source, Action<IMapperActionDescriptor<TTargetMember, TSourceMember>> configure)
        where TTargetMember : class, new()
        where TSourceMember : class, new()
    {
        var profile = new MapperProfileDefault<TTargetMember, TSourceMember>(configure);

        if (this.Profiles.Contains(profile))
        {
            throw new Exception("");
        }

        IMapperActionDescriptor descriptor = new MapperActionDescriptor<TTargetMember, TSourceMember>()
        {
            Profiles = this.Profiles
        };

        profile.Configure(descriptor);

        this.Profiles.Add(profile);

        var action = new MapperActionNestedObject<TTarget, TTargetMember, TSource, TSourceMember>(target, source)
        {
            Profile = profile
        };

        this.MapAction(action);

        return this;
    }

    public IMapperActionDescriptor<TTarget, TSource> AfterMap(Action<TTarget, TSource> action)
    {
        this.PostActions.Add(new MapperAction<TTarget, TSource>(action));
        return this;
    }
    public IMapperActionDescriptor<TTarget, TSource> BeforeMap(Action<TTarget, TSource> action)
    {
        this.PreActions.Add(new MapperAction<TTarget, TSource>(action));
        return this;
    }


    public IMapperActionDescriptor<TTarget, TSource> MapMembers(string target, string source)
    {
        var targetParameter = Expression.Parameter(typeof(TTarget));
        var sourceParameter = Expression.Parameter(typeof(TSource));
        
        var targetParameterMember = targetParameter.GetMemberExpression(target);
        var sourceParameterMember = sourceParameter.GetMemberExpression(source);
       
        var targetLambda = Expression.Lambda(targetParameterMember, targetParameter);
        var sourceLambda = Expression.Lambda(sourceParameterMember, sourceParameter);

        var mapperActionType = typeof(MapperActionMember<,,,>).MakeGenericType(
            typeof(TTarget),
            targetParameterMember.Type,
            typeof(TSource), 
            sourceParameterMember.Type);

        var mapperAction = Activator.CreateInstance(mapperActionType, targetLambda, sourceLambda) as IMapperAction;
        
        this.MapAction(mapperAction);

        return this;
    }
    public IMapperActionDescriptor<TTarget, TSource> MapMembers<TTargetMember, TSourceMember>(Expression<Func<TTarget, TTargetMember>> target, Expression<Func<TSource, TSourceMember>> source)
        where TSourceMember : TTargetMember
    {
        var action = new MapperActionMember<TTarget, TTargetMember, TSource, TSourceMember>(target, source);

        //if (Current.MapActions.Contains(action))
        //{
        //    throw new Exception("There is a duplicate action");
        //}

        this.MapAction(action);

        return this;
    }
}
