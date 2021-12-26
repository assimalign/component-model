using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal;


internal sealed class ValidationCollectionRule<T, TCollection> : 
    IValidationCollectionRule<T, TCollection> where TCollection : IEnumerable
{
    // long - index of rule | string - message for indexed rule
    private readonly IDictionary<long, string> codes = new Dictionary<long, string>();
    private readonly IDictionary<long, string> messages = new Dictionary<long, string>();


    /// <summary>
    /// 
    /// </summary>
    public Expression<Func<T, TCollection>> Collection { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IValidationRuleStack CollectionRules { get; }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    //public IValidator<TCollection> Validator { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rule"></param>
    public IValidationCollectionRule<T, TCollection> AddRule(IValidationRule rule)
    {
        this.CollectionRules.Push(rule);
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var values = this.Collection.Compile();

            foreach (var value in values.Invoke(instance))
            {

            }
        }

        throw new NotImplementedException();
    }
}

