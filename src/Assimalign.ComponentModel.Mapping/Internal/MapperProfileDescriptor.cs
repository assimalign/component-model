using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Internal
{
    using Assimalign.ComponentModel.Mapping.Abstractions;


    internal sealed class MapperProfileDescriptor<TSource, TTarget> : 
        IMapperProfileDescriptor<TSource, TTarget>
    {
        public MapperProfileDescriptor()
        {

        }

        public void AfterMap(Action<TSource, TTarget> action)
        {
            
        }

        public void BeforeMap(Action<TSource, TTarget> action)
        {
            
        }

        public void DisableDefaultMapping()
        {
            
        }

        public IMapperProfileTargetDescriptor<TTarget> ForSource(string member)
        {
            var descriptor = new MapperProfileTargetDescriptor<TTarget>();



            return descriptor;
        }

        public IMapperProfileTargetDescriptor<TTarget> ForSource<TSourceMember>(Expression<Func<TSource, TSourceMember>> expression)
        {
            var descriptor = new MapperProfileTargetDescriptor<TTarget>();



            return descriptor;
        }

        public IMapperProfileSourceDescriptor<TSource> ForTarget(string member)
        {
            var descriptor = new MapperProfileSourceDescriptor<TSource>();



            return descriptor;
        }

        public IMapperProfileSourceDescriptor<TSource> ForTarget<TTargetMember>(Expression<Func<TTarget, TTargetMember>> expression)
        {
            var descriptor = new MapperProfileSourceDescriptor<TSource>();



            return descriptor;
        }
    }
}
