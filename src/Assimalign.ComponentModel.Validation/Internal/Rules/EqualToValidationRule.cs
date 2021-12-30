using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal.Rules;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;
using Assimalign.ComponentModel.Validation.Internal.Extensions;

internal sealed class EqualToValidationRule<TValue, TArgument> : ValidationRuleBase<TValue>
    where TArgument : notnull, IEquatable<TArgument>
{
    private readonly TArgument argument;

    public EqualToValidationRule(TArgument argument)
    {
        this.argument = argument;      
    }

    public override string Name => $"EqualToValidationRule<>";

    public override string Name { set => throw new NotImplementedException(); }

    public override bool TryValidate(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }

    public override bool TryValidate(TValue value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
}

