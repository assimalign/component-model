
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping;

using Assimalign.ComponentModel.Mapping.Internal;
using Assimalign.ComponentModel.Mapping.Abstractions;

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




    public void AddProfile<TSource, TTarget>(IMapperProfile<TSource, TTarget> profile)
    {
        if (profiles.Contains(profile))
        {
            throw new Exception("");
        }

        var descriptor = new MapperProfileDescriptor<TSource, TTarget>()
        {
            Profiles = profiles,
            Current = profile
        };

        profile.Configure(descriptor);
    }
}
