using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Internal;

internal sealed class MapperProfileTargetDescriptor<TSource, TSourceMember, TTarget> :
    IMapperProfileTargetDescriptor<TSource, TTarget>
{

    public MapperProfileTargetDescriptor()
    {

    }

    public IMapperAction MapperAction { get; set; }

    public IMapperProfileDescriptor<TSource, TTarget> Parent { get; set; }


    public IMapperProfileDescriptor<TSource, TTarget> Ingore()
    {
        Parent.AddMapperAction(MapperAction);
        return Parent;
    }

    public IMapperProfileDescriptor<TSource, TTarget> MapTarget<TTargetMember>(Expression<Func<TTarget, TTargetMember>> expression)
    {
        Parent.AddMapperAction(MapperAction);
        return Parent;
    }

    public IMapperProfileDescriptor<TSource, TTarget> MapTarget(string member)
    {

        return Parent;
    }
}
