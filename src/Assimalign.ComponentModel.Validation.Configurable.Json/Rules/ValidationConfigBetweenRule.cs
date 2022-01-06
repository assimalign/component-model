using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal.Rules;

internal class ValidationConfigBetweenRule : ValidationConfigJsonRule
{

    [JsonPropertyName("$lower")]
    public object LowerBound { get; set; }

    [JsonPropertyName("$$upper")]
    public object UpperBound { get; set; }
}

