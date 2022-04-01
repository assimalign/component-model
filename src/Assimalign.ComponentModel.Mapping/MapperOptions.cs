
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.ComponentModel.Mapping;

using Assimalign.ComponentModel.Mapping.Internal;

/// <summary>
/// 
/// </summary>
public sealed partial class MapperOptions
{
    private readonly IList<IMapperProfile> profiles;

    /// <summary>
    /// 
    /// </summary>
    public MapperOptions()
    {
        this.profiles = new List<IMapperProfile>();
    }

    /// <summary>
    /// 
    /// </summary>
    public MapperMemberHandling MemberHandling { get; set; } = MapperMemberHandling.Override;
    /// <summary>
    /// 
    /// </summary>
    public MapperCollectionHandling CollectionHandling { get; set; } = MapperCollectionHandling.Override;
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<IMapperProfile> Profiles => profiles;


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="profile"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public MapperOptions AddProfile<TTarget, TSource>(IMapperProfile<TTarget, TSource> profile)
    {
        if (profiles.Any(x => x.SourceType == typeof(TSource) && x.TargetType == typeof(TTarget)))
        {
            throw new Exception($"A profile with the same target type: '{profile.TargetType.Name}' and source type: '{profile.SourceType.Name}' has already been added.");
        }

        profiles.Add(profile);

        IMapperActionDescriptor descriptor = new MapperActionDescriptor<TTarget, TSource>()
        {
            Profiles = profiles,
            MapActions = profile.MapActions
        };

        profile.Configure(descriptor);

        return this;
    }
}
