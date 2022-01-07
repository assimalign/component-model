﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Resolvers;

namespace Assimalign.ComponentModel.Validation.Configurable;

internal abstract class ValidationConfigJsonRule : IValidationRule
{
    [JsonPropertyName("$rule")]
    public string Name { get; set; }

    public abstract bool TryValidate(object value, out IValidationContext context);
}