using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Internal
{
    using Assimalign.ComponentModel.Mapping.Abstractions;


    internal sealed class MapperProfileDescriptor : IMapperProfileDescriptor
    {
        public MapperProfileDescriptor(MapperProfileContext context)
        {
            this.Context = context;
        }

        public MapperProfileContext Context { get; }

        public IMapperProfileDescriptor<TSource, TTarget> Create<TSource, TTarget>()
        {
            return new MapperProfileDescriptor<TSource, TTarget>(Context);
        }
    }

    internal sealed class MapperProfileDescriptor<TSource, TTarget> : 
        IMapperProfileDescriptor<TSource, TTarget>
    {
        private readonly Type sourceType = typeof(TSource);
        private readonly Type targetType = typeof(TTarget);

        public MapperProfileDescriptor(MapperProfileContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        public MapperProfileContext Context { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void AfterMap(Action<TSource, TTarget> action)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void BeforeMap(Action<TSource, TTarget> action)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public void DisableDefaultMapping()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IMapperProfileDescriptor<TSource, TTarget> ForMember(string source, string target)
        {
            var sourceMember = sourceType.GetMember(source, BindingFlags.IgnoreCase);



            throw new NotImplementedException();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSourceMember"></typeparam>
        /// <typeparam name="TTargetMember"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IMapperProfileDescriptor<TSource, TTarget> ForMember<TSourceMember, TTargetMember>(
            Expression<Func<TSource, TSourceMember>> source, 
            Expression<Func<TTarget, TTargetMember>> target)
        {

            if (source.Body is MemberExpression sourceMember && 
                target.Body is MemberExpression targetMember)
            {
                Action<TSource, TTarget> mapperAction = (sourceItem, targetItem) =>
                {
                    var sourceValue = source.Compile().Invoke(sourceItem);

                    if (targetMember.Member is PropertyInfo property)
                    {
                        property.SetValue(targetItem, sourceValue);
                    }

                };

                this.Context.MapperActions.Add(mapperAction);
            }



            //var sourceProperties = sourceType.GetProperties();
            //var targetProperties = targetType.GetProperties();

            //var properties = (from s in sourceProperties
            //                  from t in targetProperties
            //                  where s.Name == t.Name &&
            //                      s.CanRead &&
            //                      t.CanWrite &&
            //                      s.PropertyType.IsPublic &&
            //                      t.PropertyType.IsPublic &&
            //                      s.PropertyType == t.PropertyType &&
            //                      (
            //                          (s.PropertyType.IsValueType &&
            //                          t.PropertyType.IsValueType
            //                          ) ||
            //                          (s.PropertyType == typeof(string) &&
            //                          t.PropertyType == typeof(string)
            //                          )
            //                      )
            //                  select new PropertyMap
            //                  {
            //                      SourceProperty = s,
            //                      TargetProperty = t
            //                  }).ToList();


            ///*
            //    When using the 'ForMember' the parameters:
            //        1.  MUST BE MemberExpressions
            //*/

            //if (source.Body is LambdaExpression sourceLambda &&  target.Body is LambdaExpression targetLambda)
            //{
            //    if (sourceLambda.Body is MemberExpression sourceMember || targetLambda.Body is MemberExpression targetMember)
            //    {
            //        Action<TSource, TTarget> mapping = (sourceItem, targetItem) =>
            //        {
            //            var targetType = typeof(TTarget);

            //            var member = targetType.GetMember("").FirstOrDefault();

            //            if (member is PropertyInfo property)
            //            {
            //               // property.
            //            }
            //        };
            //    }
            //    else
            //    {
            //        throw new Exception("Invalid Expression");
            //    }
            //}
            //else
            //{
            //    throw new Exception();
            //}

            return this;
        }







        public IMapperProfileDescriptor<TSource, TTarget>  Create<TSource, TTarget>()
        {
            throw new NotImplementedException();
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

        public void ReverseMap()
        {
            throw new NotImplementedException();
        }

        public IMapperProfileDescriptor<TSourceMember, TTargetMember> AddProfile<TSourceMember, TTargetMember>(Expression<Func<TSource, IEnumerable<TSourceMember>>> source, Expression<Func<TTarget, IEnumerable<TTargetMember>>> target)
        {

            var profile = new MapperProfileDefault<TSourceMember, TTargetMember>();

            this.Context.AddSubProfile(profile);

            var descriptor = new MapperProfileDescriptor<TSourceMember, TTargetMember>(profile.Context);

            return descriptor;
        }

        public IMapperProfileDescriptor<TSourceMember, TTargetMember> AddProfile<TSourceMember, TTargetMember>(Expression<Func<TSource, TSourceMember>> source, Expression<Func<TTarget, TTargetMember>> target)
        {
            throw new NotImplementedException();
        }
    }
}
