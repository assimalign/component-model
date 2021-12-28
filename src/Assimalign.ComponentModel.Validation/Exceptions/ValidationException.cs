using System;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// 
/// </summary>
public abstract class ValidationException : Exception
{
    private const string messageDefault = "An unknown error was caught.";

    /// <summary>
    /// 
    /// </summary>
    public ValidationException() : this(messageDefault)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public ValidationException(string message) : base(message)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public ValidationExceptionCode ErrorCode { get; set; } = ValidationExceptionCode.UnknownError;

    /// <summary>
    /// 
    /// </summary>
    public override string Source { get; set; }
}

