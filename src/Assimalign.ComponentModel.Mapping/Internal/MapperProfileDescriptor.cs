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
            if (source.Body is MemberExpression sourceMember && target.Body is MemberExpression targetMember)
            {
                var sourceFunc = source.Compile();
                var targetFunc = target.Compile();

                // Check if mapped types match
                if (targetMember.Type != sourceMember.Type)
                {
                    throw new InvalidCastException(
                        $"Cannot implicitly convert '{sourceMember.Type}' to '{targetMember.Type}'. This is a transformation in must be done in the 'ForTarget()' or 'ForSource()' APIs.");
                }
                if (targetMember.Member is PropertyInfo targetProperty && sourceMember.Member is PropertyInfo sourceProperty)
                {
                    // Create the 'TSource' -> 'TTarget' Mapping
                    Action<TSource, TTarget> forwardMapperAction = (sourceInstance, targetInstance) =>
                    {
                        var sourceValue = sourceFunc.Invoke(sourceInstance);

                        targetProperty.SetValue(targetInstance, sourceValue);
                    };

                    // Create the reverse 'TSource' <- 'TTarget' Mapping
                    Action<TTarget, TSource> reverseMapperAction = (targetInstance, sourceInstance) =>
                    {
                        var targetValue = targetFunc.Invoke(targetInstance);

                        sourceProperty.SetValue(sourceInstance, targetValue);
                    };
                }
                else
                {
                    // TODO: Throw Internal Exception
                }

                // this.Context.MapperActions.Add(mapperAction);

                // .. TODO: Need to implement Mapper Profile Paths for Class to add delegates to
            }
            else
            {
                throw new ArgumentException("'ForMember' only excepts Lambda Expression that return a Member Expression");
            }

            return this;
        }







        IMapperProfileDescriptor<TSource, TTarget> IMapperProfileDescriptor.Create<TSource, TTarget>()
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

            Action<TSource, TTarget> forwardMapperAction = (sourceInstance, targetInstance) =>
            {
                var sourceValue = sourceFunc.Invoke(sourceInstance);

                targetProperty.SetValue(targetInstance, sourceValue);
            };


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



    public sealed class MapperReferences<T>
    {

    }
}
