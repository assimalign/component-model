namespace Assimalign.ComponentModel.Validation.Internal;


/// <summary>
/// The validation rule type is set to specify what rule type is being described.
/// </summary>
internal enum ValidationRuleType
{
    /// <summary>
    /// A singular rule is used when applying one or more 
    /// validation rules to a single item. This is set when using
    /// the <see cref="IValidationRuleDescriptor{T}.RuleFor{TValue}(System.Linq.Expressions.Expression{System.Func{T, TValue}})"/>.
    /// </summary>
    SingularRule = 0,
    /// <summary>
    /// A recursive rule is used when applying one or more of the same 
    /// validation rule(s) to multiple items. This is set when using 
    /// the <see cref="IValidationRuleDescriptor{T}.RuleForEach{TValue}(System.Linq.Expressions.Expression{System.Func{T, System.Collections.Generic.IEnumerable{TValue}}})"/>.
    /// </summary>
    RecursiveRule = 1,
    /// <summary>
    /// A conditional rule is used when applying a predicate 
    /// that specifies whether one or more validation rules should be evaluated. 
    /// This is set when using the <see cref="IValidationRuleDescriptor{T}.When(System.Linq.Expressions.Expression{System.Func{T, bool}}, System.Action{IValidationRuleDescriptor{T}})"/>.
    /// </summary>
    ConditionalRule = 2,
    /// <summary>
    /// 
    /// </summary>
    Other = 3
}