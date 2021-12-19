using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping
{
    using Assimalign.ComponentModel.Mapping.Abstractions;
    using System.Diagnostics.CodeAnalysis;

    public abstract class MapperProfile<TSource, TTarget> : 
        IMapperProfile<TSource, TTarget>
    {

        private readonly IEnumerable<Delegate> after;
        private readonly IEnumerable<Delegate> before;

        public MapperProfile()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        public IMapperSource Source { get; }

        /// <summary>
        /// 
        /// </summary>
        public IMapperTarget Target { get; }

        /// <summary>
        /// 
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
        /// <exception cref="NotImplementedException"></exception>
        public static IMapperProfile<TSource, TTarget> Create<TSource, TTarget>(Action<IMapperProfile<TSource, TTarget>> configure)
        {
            throw new NotImplementedException();
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

        public bool Equals([AllowNull] IMapperProfile x, [AllowNull] IMapperProfile y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] IMapperProfile profile)
        {
            if (this.Target.TargetType == profile.Target.TargetType &&
                this.Source.SourceType == profile.Source.SourceType)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
