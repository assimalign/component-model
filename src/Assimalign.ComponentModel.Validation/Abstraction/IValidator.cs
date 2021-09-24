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
    }
}
