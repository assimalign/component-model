using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class LengthBetweenValidationRule<T, TValue, TArgument> : IValidationRule
{
    private readonly TArgument lowerBound;
    private readonly TArgument upperBound;
    private readonly Expression<Func<T, TValue>> expression;


    public LengthBetweenValidationRule(Expression<Func<T, TValue>> expression, TArgument lowerBound, TArgument upperBound)
    {
        this.upperBound = upperBound;
        this.lowerBound = lowerBound;
        this.expression = expression;
    }

    public string Name => throw new NotImplementedException();

    public IValidationError Error { get; set; }

    public void Evaluate(IValidationContext context)
    {
        throw new NotImplementedException();
    }
}
