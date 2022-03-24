using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.ComponentModel.Mapping;


using Assimalign.ComponentModel.Mapping.Internal;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TTarget"></typeparam>
public abstract class MapperProfile<TSource, TTarget> :
    IMapperProfile<TSource, TTarget>
{

    private readonly IList<IMapperAction> actions;

    public MapperProfile()
    {
        this.actions = new List<IMapperAction>();
    }

    /// <inheritdoc cref="IMapperProfile.SourceType"/>
    public Type SourceType => typeof(TSource);
    
    /// <inheritdoc cref="IMapperProfile.TargetType"/>
    public Type TargetType => typeof(TTarget);

    /// <inheritdoc cref="IMapperProfile.Actions"/>
    public IEnumerable<IMapperAction> Actions => this.actions;

    /// <inheritdoc cref="IMapperProfile{TSource, TTarget}.Configure(IMapperProfileDescriptor{TSource, TTarget})"/>
    public abstract void Configure(IMapperProfileDescriptor<TSource, TTarget> descriptor);

    /// <inheritdoc cref="IMapperProfile.Configure(IMapperProfileDescriptor)"/>
    public void Configure(IMapperProfileDescriptor descriptor)
    {
        if (descriptor is IMapperProfileDescriptor<TSource, TTarget> desc)
        {
            this.Configure(desc);
        }
        else
        {
            throw new ArithmeticException("");
        }
    }

    public bool Equals(IMapperProfile profile) => this.SourceType == profile.SourceType && this.TargetType == profile.TargetType;
    public bool Equals(IMapperProfile left, IMapperProfile right) => left.Equals(right);
    public override bool Equals(object obj) => obj is IMapperProfile profile ? Equals(profile) : false;
    public int GetHashCode([DisallowNull] IMapperProfile profile) => profile.GetHashCode();
    public override int GetHashCode() => HashCode.Combine(this.SourceType, this.TargetType);
}

//private IDictionary<string, Type> GetPaths(Type type)
//{
//    var paths = new Dictionary<string, Type>();

//    foreach (var property in type.GetProperties().Where(x => x.CanWrite && x.CanRead))
//    {
//        if (property.PropertyType.IsValueType())
//        {
//            paths.Add(property.Name, property.PropertyType);
//        }
//        else if (property.PropertyType.IsComplexType())
//        {
//            foreach (var child in GetPaths(property.PropertyType))
//            {
//                paths.Add($"{property.Name}.{child.Key}", child.Value);
//            }
//        }
//        else if (property.PropertyType.IsEnumerableType(out var implementationType))
//        {

//            if (implementationType.IsComplexType())
//            {
//                foreach (var child in GetPaths(implementationType))
//                {
//                    paths.Add($"{property.Name}.[{child.Key}]", child.Value);
//                }
//            }
//        }
//    }

//    return paths;
//}