using System;
using System.Collections.Generic;
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

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public Mapper(MapperOptions options)
        {
            this.options = options;
            this.profiles = new List<IMapperProfile>();
        }

        /* Flow
         * 1. Iterate through source properties
         */



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object Map(object source, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object Map(object source, object destination, Type sourceType, Type destinationType)
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
