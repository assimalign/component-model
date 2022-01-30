using System;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal.Exceptions;

internal sealed class ValidationConfigurableJsonInvalidEvaluationException : ValidationConfigurableException
{
    private const string message = "The following expression: '{0}' cannot be evaluated with rule: '{1}'.";

    public ValidationConfigurableJsonInvalidEvaluationException(string expression, string rule) 
        : base(string.Format(message, expression, rule)) { }


    public ValidationConfigurableJsonInvalidEvaluationException(LambdaExpression expression, string rule)
        : base(string.Format(message, expression.ToString(), rule))
    {
        this.Source = expression.Body.ToString();
    }

}

