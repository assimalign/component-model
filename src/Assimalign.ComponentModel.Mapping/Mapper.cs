using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping
{
    using Assimalign.ComponentModel.Mapping.Internal;
    using Assimalign.ComponentModel.Mapping.Abstractions;
    

    /// <summary>
    /// 
    /// </summary>
    public sealed class Mapper : IMapper
    {
        private readonly MapperOptions options;
        private readonly IList<IMapperProfile> profiles;
        private readonly static ConcurrentDictionary<Type, MapperPaths> flattenedTypes;

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public Mapper(MapperOptions options)
        {
            this.options = options;
            this.profiles = options.Profiles.ToList();
        }

        /* Flow
         * 1. Iterate through source properties
         */



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TTarget Map<TSource, TTarget>(TSource source)
            where TTarget : new()
        {
            var results = this.Map(source, typeof(TSource), typeof(TTarget));

            if (results is TTarget target)
            {
                return target;
            }
            else
            {
                throw new Exception("");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TTarget Map<TSource, TTarget>(TSource source, TTarget target)
        {
            var mapperResults = this.Map(source, target, typeof(TSource), typeof(TTarget));

            if (mapperResults is TTarget target1)
            {
                return target1;
            }
            else
            {
                throw new Exception("");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object Map(object source, Type sourceType, Type targetType)
        {


            var profile = this.profiles.FirstOrDefault(x => x.Context.SourceType == sourceType && x.Context.TargetType == targetType);




            foreach (var action in profile.Context.)
            {
                
                if (action is Action<TSource, TTarget> mapperAction)
                {
                    mapperAction.Invoke(source, target);
                }
            }


            return target;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object Map(object source, object target, Type sourceType, Type targetType)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IMapper Create(Action<MapperOptions> configure)
        {
            var options = new MapperOptions();

            configure.Invoke(options);

            return new Mapper(options);
        }
    }
}
