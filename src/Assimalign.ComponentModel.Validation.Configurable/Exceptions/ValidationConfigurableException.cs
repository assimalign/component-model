using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

/// <summary>
/// 
/// </summary>
public abstract class ValidationConfigurableException : Exception
{

    public ValidationConfigurableException(string message) 
        : base(message) { }

    public ValidationConfigurableException(string message, string source) 
        : base(message)
    {
        this.Source = source;
    }

    public ValidationConfigurableException(string message, Exception innerException) 
        : base(message, innerException) { }

    public override string Source { get; set; }

}
