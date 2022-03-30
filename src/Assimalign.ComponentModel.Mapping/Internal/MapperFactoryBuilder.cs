using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Internal;

internal sealed class MapperFactoryBuilder : IMapperFactoryBuilder
{

    public MapperFactoryBuilder()
    {
        this.Profiles = new List<IMapperProfile>();
    }

    public IList<IMapperProfile> Profiles { get; set; }

    public IMapper Build()
    {
        throw new NotImplementedException();
    }

    public IMapperFactoryBuilder Configure(Action<IMapperActionDescriptor> configure)
    {
        throw new NotImplementedException();
    }

    public IMapperFactoryBuilder Configure<TTarget, TSource>(Action<IMapperProfile<TTarget, TSource>> configure)
    {
        var profile = new MapperProfileDefault<TTarget, TSource>();
        var descriptor = new MapperActionDescriptor<TTarget, TSource>()
        {
            Profiles = this.Profiles
        };

        profile.Configure(descriptor);

        Profiles.Add(profile);

        return this;
    }
}
