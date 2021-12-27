using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;
internal sealed class LengthValidationRule<T, TValue, TArgument> : IValidationRule
{
    private readonly TArgument argument;
    private readonly Expression<Func<T, TValue>> expression;


    public LengthValidationRule(Expression<Func<T, TValue>> expression, TArgument argument)
    {
        this.argument = argument;
        this.expression = expression;
    }

    public string Name => throw new NotImplementedException();

    public IValidationError Error { get; set; }

    public void Evaluate(IValidationContext context)
    {
        throw new NotImplementedException();
    }
}

