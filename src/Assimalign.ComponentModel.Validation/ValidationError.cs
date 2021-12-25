using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// 
/// </summary>
public class ValidationError : IValidationError
{

    public ValidationError()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="error"></param>
    internal ValidationError(IValidationError error)
    {
        this.Code = error.Code;
        this.Message = error.Message;
        this.Source = error.Source;
    }


    /// <summary>
    /// A unique error code for the validation error.
    /// </summary>
    public string Code { get; set; } = "400";

    /// <summary>
    /// 
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// The source of the validation error such as a member of method.
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"Error {Code}: {Message} {Environment.NewLine} └─> Source: {Source}";
    }
}

