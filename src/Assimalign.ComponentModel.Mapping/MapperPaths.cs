using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping;

public record MapperBinder<TContract, TBinding>
{
    public Type ContractType { get; set; }
    public Type ContractMemberType { get; set; }
    public Type BindingType { get; set; }
}
public interface IMapperItem
{
    IMapperSourceItem SourceItem { get; }
    IMapperTargetItem TargetItem { get; }
}
public interface IMapperSourceItem
{
    Type SourceType { get; }
    Expression SourceExpression { get; }
    object GetValue(object instance);
}
public interface IMapperTargetItem : 
    IEquatable<IMapperTargetItem>
{
    Type TargetType { get; }
    Expression Target { get; }

    void SetValue(object instance, object value);
}
public interface IMapperItem<in TSource, in TTarget> : IMapperTargetItem
{

    object GetValue<TValue>(TSource source);
    void SetValue<TValue>(TTarget target, TValue value);
}
public sealed class MemberItem : IMapperTargetItem
{

    public MemberItem(LambdaExpression source, LambdaExpression target)
    {

        source.Compile().
    }
    
    public MemberItem(Expression sourceExpression, Expression targetExpression)
    {
        if (sourceExpression is LambdaExpression lambda
        if (sourceExpression is MemberExpression memberExpression)
        {
            if (memberExpression.Member is PropertyInfo property)
            {
                //property.
            }
        }
        else
        {
            throw new NotSupportedException($"The expression type '{sourceExpression}' is not supported.");
        }
    }

    public Type SourceType { get; }
    public Expression Source { get; }
    public Type TargetType { get; }
    public Expression Target { get; }

    public void SetTargetValue(object instance, object value)
    {
        throw new NotImplementedException();
    }

    public bool Equals(IMapperTargetItem other)
    {
        throw new NotImplementedException();
    }
}
public sealed class MemberItemGetter
{
    // getter 
}
public sealed class MemberItemSetter
{

}
public class MapperItem<T, TValue>
{

    public MapperItem()
    {

    }


    public LambdaExpression Delegate { get; set; }

    public TValue GetValue(T instance)
    {
        if (Delegate is Expression<Func<T, TValue>> lambda)
        {
            return lambda.Compile().Invoke(instance);
        }
        else
        {
            return default;
        }
    }
}

/// <summary>
/// 
/// </summary>
public sealed class MapperPaths : IDictionary<string, Type>
{
    private int count;
    private string[] keys;
    private Type[] values;

    public MapperPaths()
    {

    }



    public Type this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    /// <summary>
    /// 
    /// </summary>
    public ICollection<string> Keys => this.keys;

    /// <summary>
    /// 
    /// </summary>
    public ICollection<Type> Values => this.Values;

    /// <summary>
    /// 
    /// </summary>
    public int Count => this.count;

    /// <summary>
    /// 
    /// </summary>
    public bool IsReadOnly => throw new NotImplementedException();

    public void Add(string key, Type value)
    {

        throw new NotImplementedException();
    }

    public void Add(KeyValuePair<string, Type> item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(KeyValuePair<string, Type> item)
    {
        throw new NotImplementedException();
    }

    public bool ContainsKey(string key)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<string, Type>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<string, Type>> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public bool Remove(string key)
    {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<string, Type> item)
    {
        throw new NotImplementedException();
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out Type value)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }




    public static MapperPaths Create(Type type)
    {
        throw new NotImplementedException();
    }
}
