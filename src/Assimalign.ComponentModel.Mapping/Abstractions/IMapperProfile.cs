using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMapperProfile : IEqualityComparer<IMapperProfile>
    {
        /// <summary>
        /// 
        /// </summary>
        IMapperSource Source { get; }

        /// <summary>
        /// 
        /// </summary>
        IMapperTarget Target { get; }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public interface IMapperProfile<TSource, TTarget> : IMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptor"></param>
        void Configure(IMapperProfileDescriptor<TSource, TTarget> descriptor);
    }
}
