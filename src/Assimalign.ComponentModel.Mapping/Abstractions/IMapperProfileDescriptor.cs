using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Abstractions
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public interface IMapperProfileDescriptor<TSource, TTarget> 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        IMapperProfileTargetDescriptor<TTarget> ForSource(string member);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSourceMember"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        IMapperProfileTargetDescriptor<TTarget> ForSource<TSourceMember>(Expression<Func<TSource, TSourceMember>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        IMapperProfileSourceDescriptor<TSource> ForTarget(string member);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTargetMember"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        IMapperProfileSourceDescriptor<TSource> ForTarget<TTargetMember>(Expression<Func<TTarget, TTargetMember>> expression);

        /// <summary>
        /// This will disable default mapping of the 'TTarget' and 'TSource' on compiling the profile.
        /// Id disabled then each property must be mapped individually.
        /// </summary>
        void DisableDefaultMapping();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        void BeforeMap(Action<TSource, TTarget> action);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        void AfterMap(Action<TSource, TTarget> action);
    }
}
