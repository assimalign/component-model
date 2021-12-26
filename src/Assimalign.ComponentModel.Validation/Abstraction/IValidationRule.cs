namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// 
/// </summary>
public interface IValidationRule
{
    /// <summary>
    /// A unique name for the rule to evaluate.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Runs the validation rule against the given context.
    /// </summary>
    /// <param name="context"></param>
    void Evaluate(IValidationContext context);
}

