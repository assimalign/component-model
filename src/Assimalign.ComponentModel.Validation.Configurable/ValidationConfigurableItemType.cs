namespace Assimalign.ComponentModel.Validation.Configurable;

/// <summary>
/// An Enum type representing the Descriptor Rule API(s).
/// </summary>
public enum ValidationConfigurableItemType
{
    /// <summary>
    /// Represents the  <see cref="IValidationRuleDescriptor{T}.RuleForEach{TValue}(System.Linq.Expressions.Expression{System.Func{T, System.Collections.Generic.IEnumerable{TValue}}})"/>
    /// </summary>
    Recursive = 0,

    /// <summary>
    /// Represents the <see cref="IValidationRuleDescriptor{T}.RuleFor{TValue}(System.Linq.Expressions.Expression{System.Func{T, TValue}})"/>
    /// </summary>
    Inline = 1
}