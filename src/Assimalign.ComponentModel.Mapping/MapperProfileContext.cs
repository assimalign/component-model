using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping;

using Assimalign.ComponentModel.Mapping.Abstractions;

/// <summary>
/// 
/// </summary>
public sealed class MapperProfileContext : IEquatable<MapperProfileContext>
{
    private readonly IList<IMapperProfile> profiles;

    internal MapperProfileContext()
    {
        this.profiles = new List<IMapperProfile>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sourceType"></param>
    /// <param name="targetType"></param>
    public MapperProfileContext(
        Type sourceType,
        Type targetType) : this()
    {
        this.SourceType = sourceType;
        this.TargetType = targetType;
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<IMapperTargetItem> MapperItems { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public Type SourceType { get; }
    /// <summary>
    /// 
    /// </summary>
    public MapperPaths SourcePaths { get; }
    /// <summary>
    /// 
    /// </summary>
    public Type TargetType { get; }
    /// <summary>
    /// 
    /// </summary>
    public MapperPaths TargetPaths { get; }

    /// <summary>
    /// Nested profiles are mapped profiles of nested child types. This is usually added
    /// when the 'AddProfile' API is called on <see cref="IMapperProfileDescriptor{TSource, TTarget}"/>.
    /// <list type="bullet">
    /// <item>
    ///     <see cref="IMapperProfileDescriptor{TSource, TTarget}.AddProfile{TSourceMember, TTargetMember}(Expression{Func{TSource, TSourceMember}}, Expression{Func{TTarget, TTargetMember}})"/>
    /// </item>
    /// <item>
    ///     <seealso cref="IMapperProfileDescriptor{TSource, TTarget}.AddProfile{TSourceMember, TTargetMember}(Expression{Func{TSource, IEnumerable{TSourceMember}}}, Expression{Func{TTarget, IEnumerable{TTargetMember}}})"/>
    /// </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public IEnumerable<IMapperProfile> Profiles => this.profiles;


    internal void AddSubProfile(IMapperProfile profile)
    {
        this.profiles.Add(profile);
    }



    public static MapperProfileContext New<TSource, TTarget>()
    {
        return new MapperProfileContext(typeof(TSource), typeof(TTarget));
    }


    public bool Equals(MapperProfileContext other)
    {
        if (other is null)
        {
            return false;
        }

        return this.SourceType == other.SourceType && this.TargetType == other.TargetType;
    }

    public override bool Equals(object obj)
    {
        return this.Equals(obj as MapperProfileContext);
    }
    // We need to override the Hash Code
    // Method since we overrode the Equality method
    public override int GetHashCode()
    {
        return HashCode.Combine(SourceType, TargetType);
    }
}
