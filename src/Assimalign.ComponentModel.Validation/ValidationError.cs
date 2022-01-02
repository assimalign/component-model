using System;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// an implementation of a validation failure.
/// </summary>
public sealed class ValidationError : IValidationError
{

    /// <summary>
    /// The default constructor.
    /// </summary>
    public ValidationError() { }

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
    /// A unique error message for the validation error.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// The source of the validation error such as a member of method.
    /// </summary>
    public string Source { get; set; }

    public override string ToString()
    {
        return $"Error {Code}: {Message} {Environment.NewLine} └─> Source: {Source}";
    }
}

