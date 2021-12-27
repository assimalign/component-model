﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

using Assimalign.ComponentModel.Validation;

internal sealed class NullValidationRule<T, TValue> : IValidationRule
{
    private readonly Expression<Func<T, TValue>> expression;

    public NullValidationRule(Expression<Func<T, TValue>> expression)
    {
        this.expression = expression;
    }


    public string Name => throw new NotImplementedException();

    public IValidationError Error { get; set; }

    public void Evaluate(IValidationContext context)
    {
        throw new NotImplementedException();
    }
}

