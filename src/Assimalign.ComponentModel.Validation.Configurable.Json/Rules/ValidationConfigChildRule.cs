using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable.Rules;


internal class ValidationConfigChildRule : ValidationConfigJsonRule
{

    [JsonPropertyName("$validationItems")]
    public IEnumerable<ValidationConfigJsonItem<object>> Items { get; set; }

    public override bool TryValidate(object value, out IValidationContext context)
    {
       
        throw new NotImplementedException();
    }
}

