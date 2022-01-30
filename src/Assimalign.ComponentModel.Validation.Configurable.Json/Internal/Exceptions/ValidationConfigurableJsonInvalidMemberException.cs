using System;


namespace Assimalign.ComponentModel.Validation.Configurable.Internal.Exceptions;

internal class ValidationConfigurableJsonInvalidMemberException : ValidationConfigurableException
{
    private const string message = "The following member path '{0}' was not found on type: '{1}'.";

    public ValidationConfigurableJsonInvalidMemberException(string member, Type type) 
        : base(string.Format(message, member, type.Name))
    {
        
    }
}

