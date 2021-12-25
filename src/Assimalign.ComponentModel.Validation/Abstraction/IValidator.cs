using System;
using System.Collections;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation;


/// <summary>
/// 
/// </summary>
public interface IValidator
{
    /// <summary>
    /// Validates the <paramref name="instance"/> for each profile that matches the <typeparamref name="T"/>.
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    ValidationResult Validate<T>(T instance);


    /// <summary>
    /// Validates the <paramref name="instance"/> for each profile that matches the <typeparamref name="T"/>.
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    Task<ValidationResult> ValidateAsync<T>(T instance);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance">The instance to validate.</param>
    /// <param name="profile">The name of the profile to use to validate the <paramref name="instance"/>.</param>
    /// <returns></returns>
    ValidationResult Validate<T>(T instance, string profile);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    ValidationResult Validate(IValidationContext context);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="profile">The name of the profile to use to validate the <see cref="IValidationContext.Instance"/>.</param>
    /// <returns></returns>
    ValidationResult Validate(IValidationContext context, string profile);
}

