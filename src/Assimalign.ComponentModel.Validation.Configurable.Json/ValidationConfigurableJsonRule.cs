using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

internal class ValidationConfigurableJsonRule<T> : IValidationRule
{
    private string name;


    public string Name
    {
        get => this.name;
        set
        {
            var name = value.ToLower();
            var validNames = Enum.GetNames(typeof(ValidationConfigurableJsonRuleName));

            if (Array.Exists(validNames, n => n.ToLower() == name))
            {
                this.name = name;
            }
            else
            {
                throw new ArgumentException($"The following rule name '{value}' is not valid.");
            }
        }
    }

    public object Lower { get; set; }
    public object Upper { get; set; }



    public bool TryValidate(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
}
