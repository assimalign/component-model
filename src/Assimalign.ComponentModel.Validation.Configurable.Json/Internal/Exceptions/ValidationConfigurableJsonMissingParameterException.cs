using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal.Exceptions;

internal sealed class ValidationConfigurableJsonMissingParameterException : ValidationConfigurableException
{
    private const string message1 = "The following rule '{0}' is missing parameter '{1}'.";
    private const string message2 = "The following rule '{0}' is missing parameters '{1}' and '{2}'.";

    public ValidationConfigurableJsonMissingParameterException(string ruleName, string param1, Expression source) 
        : base(string.Format(message1, param1))
    {
        this.Source = Source.ToString();
    }

    public ValidationConfigurableJsonMissingParameterException(string ruleName, string param1, string param2, Expression source) 
        : base(string.Format(message2, param1, param2))
    {
        this.Source = Source.ToString();
    }


    public override string Source { get; set; }
}

