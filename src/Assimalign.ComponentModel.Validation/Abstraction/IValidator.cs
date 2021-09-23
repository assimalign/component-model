using System;
using System.Collections;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Abstraction
{    

    /// <summary>
    /// 
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// In many cases validation requirements can change based on a collection of 
        /// various inputs. For example, when property A & property B is set use -> Validator A, 
        /// but when property A & property C is set then use -> Validator B. Assigning a name can be useful
        /// by breaking out validation into multiple validators rather than stuff all logic into one validator
        /// since it can represent one validation pass/failure of a larger whole.
        /// </remarks>
        string Name { get; set; }


        /// <summary>
        /// A collection of validation rules to apply 
        /// to the context being validated.
        /// </summary>
        IValidationRuleSet Rules { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        ValidationResult Validate(IValidationContext context);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<T> : IValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        ValidationResult Validate(T instance);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMember">A member expression is either a field or property of a Type.</typeparam>
        /// <param name="expression"></param>
        IValidationMemberRule<T, TMember> RuleFor<TMember>(Expression<Func<T, TMember>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="expression"></param>
        IValidationCollectionRule<T, TCollection> RuleForEach<TCollection>(Expression<Func<T, TCollection>> expression)
            where TCollection : IEnumerable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition">What condition is required</param>
        /// <param name="configure">The validation to </param>
        /// <returns></returns>
        IValidationConditionRule<T> When(Expression<Func<T, bool>> condition, Action<IValidationConditionRule<T>> configure);

    }
}
