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


        public MapperProfileContext Context { get; }

        public void AfterMap(Action<TSource, TTarget> action)
        {
            
        }

        public void BeforeMap(Action<TSource, TTarget> action)
        {
            
        }

        public void DisableDefaultMapping()
        {
            
        }

        public IMapperProfileDescriptor<TSource, TTarget> ForMember(string source, string target)
        {
            var sourceMember = sourceType.GetMember(source, BindingFlags.IgnoreCase);



            throw new NotImplementedException();
        }


        public IMapperProfileDescriptor<TSource, TTarget> ForMember<TSourceMember, TTargetMember>(Expression<Func<TSource, TSourceMember>> source, Expression<Func<TTarget, TTargetMember>> target)
        {
            var lambdaSource = source as LambdaExpression;
            var lambdaTarget = target as LambdaExpression;

            if (lambdaSource.ReturnType == lambdaTarget.ReturnType)
            {

            }
            else
            {
                // TODO: Throw invalid cast exception
            }

            throw new NotImplementedException();
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
