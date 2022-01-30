using System;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal.Exceptions
{
    internal sealed class ValidationConfigurableJsonUnsupportedRuleException : ValidationConfigurableException
    {
        private const string message = "The following rule '{0}' is not supported internally.";
        
        public ValidationConfigurableJsonUnsupportedRuleException(string ruleName, LambdaExpression expression) 
            : base(string.Format(message, ruleName))
        {
            this.Source = expression.ToString();
        }
    }
}
