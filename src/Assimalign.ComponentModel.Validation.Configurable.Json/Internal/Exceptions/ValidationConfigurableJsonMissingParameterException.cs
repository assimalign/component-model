﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal.Exceptions;

internal sealed class ValidationConfigurableJsonMissingParameterException : ValidationConfigurableException
{
    private const string message = "";

    public ValidationConfigurableJsonMissingParameterException(string message) : base(message)
    {
    }
}

