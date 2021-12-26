using System;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// Represents a validation rule for a property or field of a given type.
/// </summary>
/// <typeparam name="T">Is the type in which the property or field is a member of.</typeparam>
/// <typeparam name="TMember">Is either a property or member of type 'T'.</typeparam>
public interface IValidationMemberRule<T, TMember> : IValidationRule
{
    /// <summary>
    /// 
    /// </summary>
    Expression<Func<T, TMember>> Member { get; }

    /// <summary>
    /// The validation rules to apply to the member.
    /// </summary>
    IValidationRuleStack MemberRules { get; }

    /// <summary>
    /// A member validator to be used on top of any additional rules  
    /// </summary>
    //IValidator<TMember> Validator { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rule"></param>
    IValidationMemberRule<T, TMember> AddRule(IValidationRule rule);
}