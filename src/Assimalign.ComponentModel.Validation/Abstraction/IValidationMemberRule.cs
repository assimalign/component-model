using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Abstraction
{
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
        IEnumerable<IValidationRule> Rules { get; }

        /// <summary>
        /// A member validator to be used on top of any additional rules  
        /// </summary>
        IValidator<TMember> Validator { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> Null();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> NotNull();

        /// <summary>
        /// Validates whether the member is not empty.
        /// </summary>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> NotEmpty();

        /// <summary>
        /// Validates whether the member is empty.
        /// </summary>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> Empty();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TLeftBound"></typeparam>
        /// <typeparam name="TRightBound"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> Between<TLeftBound, TRightBound>(TLeftBound left, TRightBound right)
            where TLeftBound : struct
            where TRightBound : struct;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TLeftBound"></typeparam>
        /// <typeparam name="TRightBound"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> BetweenOrEqualTo<TLeftBound, TRightBound>(TLeftBound left, TRightBound right)
            where TLeftBound : struct
            where TRightBound : struct;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue">is of </typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> GreaterThan<TValue>(TValue value) where TValue : struct;
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> GreaterThanOrEqualTo<TValue>(TValue value) where TValue : struct;
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> LessThan<TValue>(TValue value) where TValue : struct;
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> LessThanOrEqualTo<TValue>(TValue value) where TValue : struct;
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> Equal<TValue>(TValue value) where TValue : IComparable;
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> NotEqual<TValue>(TValue value) where TValue : IComparable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validator"></param>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> UseValidator(IValidator<TMember> validator);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        IValidationMemberRule<T, TMember> AddRule(IValidationRule rule);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> WithErrorCode(string code);
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IValidationMemberRule<T, TMember> WithErrorMessage(string message);

    }
}
