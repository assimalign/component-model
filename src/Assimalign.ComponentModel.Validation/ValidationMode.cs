namespace Assimalign.ComponentModel.Validation;


/// <summary>
/// Specifies whether the validator should continue or stop on a validation failure.
/// </summary>
public enum ValidationMode
{
    /// <summary>
    /// Tells the validator to continue validation through all failures.
    /// </summary>
    Cascade = 0,

    /// <summary>
    /// Tells the validator to stop validation after first failure.
    /// </summary>
    Stop = 1
}