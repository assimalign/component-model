using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

using Assimalign.ComponentModel.Validation;

internal sealed class NullValidationRule<T, TValue> : IValidationRule
{
    public string Name => throw new NotImplementedException();

    public void Evaluate(IValidationContext context)
    {
        throw new NotImplementedException();
    }
}

