﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

internal class ValidationConfigurableJsonError : IValidationError
{
    [JsonPropertyName("$code")]
    public string Code { get; set; }

    [JsonPropertyName("$message")]
    public string Message { get; set; }

    [JsonPropertyName("$source")]
    public string Source { get; set; }
}
