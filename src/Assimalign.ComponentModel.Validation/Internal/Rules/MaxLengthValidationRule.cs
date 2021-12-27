using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


internal sealed class MaxLengthValidationRule<T, TValue, TArgument> : IValidationRule
{
    private readonly Func<TArgument, bool> isLessThan;
    private readonly Expression<Func<T, TValue>> expression;

    public MaxLengthValidationRule(Expression<Func<T, TValue>> expression, TArgument argument)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        if (argument is null)
        {
            throw new ArgumentNullException(nameof(argument));
        }

        this.expression = expression;
        //this.isLessThan = x => Compare(x, argument) < 0;
    }



    public IValidationError Error { get; set; }

    public string Name => throw new NotImplementedException();

    public void Evaluate(IValidationContext context)
    {
        throw new NotImplementedException();
    }
}

