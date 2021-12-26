using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Assimalign.ComponentModel.Validation.Internal.Rules;


internal sealed class EqaulToValidationRule<T, TValue, TValueCompare> : IValidationRule
    where TValue : IComparable
    where TValueCompare : IComparable
{

    private readonly Expression<Func<T, TValue>> expression;

    public EqaulToValidationRule(Expression<Func<T, TValue>> expression, TValueCompare compare)
    {
        this.expression = expression;
    }

    public string Name => throw new NotImplementedException();

    public void Evaluate(IValidationContext context)
    {
        throw new NotImplementedException();
    }
}

