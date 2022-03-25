
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public IEnumerable<IMapperProfile> Profiles => profiles;


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <param name="profile"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public MapperOptions AddProfile<TSource, TTarget>(IMapperProfile<TSource, TTarget> profile)
    {
        if (profiles.Any(x => x.SourceType == typeof(TSource) && x.TargetType == typeof(TTarget)))
        {
            throw new Exception($"A profile with the same target type: '{profile.TargetType.Name}' and source type: '{profile.SourceType.Name}' has already been added.");
        }

        profiles.Add(profile);

        IMapperProfileDescriptor descriptor = new MapperProfileDescriptor<TSource, TTarget>()
        {
            Profiles = profiles
        };

        profile.Configure(descriptor);

        return this;
    }
}
