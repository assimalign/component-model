using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


internal sealed class EmptyValidationRule<T, TValue> : IValidationRule
{
    private readonly string name;
    private readonly Expression<Func<T, TValue>> expression;

    public EmptyValidationRule(Expression<Func<T, TValue>> expression)
    {
        this.expression = expression;
    }


    public string Name => this.name;

    public IValidationError Error { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (value is null)
            {
                return;
            }

            if (typeof(TValue) == typeof(string))
            {

            }
            else if (typeof(TValue) == typeof(IEnumerable))
            {

            }
        }
        else
        {

        }
    }


    private TValue GetValue(T instance)
    {
        try
        {
            return expression.Compile().Invoke(instance);
        }
        catch(Exception exception)
        {
            return default(TValue);
        }
    }
}
