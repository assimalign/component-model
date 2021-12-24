using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.ComponentModel.Mapping
{
    using Assimalign.ComponentModel.Mapping.Abstractions;
    

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public abstract class MapperProfile<TSource, TTarget> : 
        IMapperProfile<TSource, TTarget>
    {
        private readonly IList<IMapperProfile> profiles = new List<IMapperProfile>();
        private readonly IList<Action<TSource, TTarget>> after = new List<Action<TSource, TTarget>>();
        private readonly IList<Action<TSource, TTarget>> before = new List<Action<TSource, TTarget>>();

        public MapperProfile(MapperProfileContext context)
        {
            this.Context = context;
        }


        public MapperProfile()
        {
            this.Context = MapperProfileContext.New<TSource, TTarget>();
        }


        /// <summary>
        /// 
        /// </summary>
        public MapperProfileContext Context { get; internal set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptor"></param>
        void IMapperProfile.Configure(IMapperProfileDescriptor descriptor)
        {
            if (descriptor is IMapperProfileDescriptor<TSource, TTarget> desc)
            {
                this.Configure(desc);
            }
            else
            {

            }
        }

        /// <summary>
        /// Invokes
        /// </summary>
        /// <param name="descriptor"></param>
        public abstract void Configure(IMapperProfileDescriptor<TSource, TTarget> descriptor);


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IMapperProfile<TSource, TTarget> Create<TSource, TTarget>(Action<IMapperProfile<TSource, TTarget>> configure)
        {
            var profile = new MapperProfileDefault<TSource, TTarget>();

            configure.Invoke(profile);

            return profile;
        }


        private IDictionary<string, Type> GetPaths(Type type)
        {
            var paths = new Dictionary<string, Type>();

            foreach(var property in type.GetProperties().Where(x=>x.CanWrite && x.CanRead))
            {
                if (property.PropertyType.IsSystemValueType())
                {
                    paths.Add(property.Name, property.PropertyType);
                }
                else if (property.PropertyType.IsComplexType())
                {
                    foreach(var child in GetPaths(property.PropertyType))
                    {
                        paths.Add($"{property.Name}.{child.Key}", child.Value);
                    }
                }
                else if (property.PropertyType.IsEnumerableType(out var implementationType))
                {

                    if (implementationType.IsComplexType())
                    {
                        foreach (var child in GetPaths(implementationType))
                        {
                            paths.Add($"{property.Name}.[{child.Key}]", child.Value);
                        }
                    }
                }
            }

            return paths;
        }

    }
}
