using System.Threading;
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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    Task<ValidationResult> ValidateAsync<T>(T instance, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance">The instance to validate.</param>
    /// <param name="profileName">The name of the profile to use to validate the <paramref name="instance"/>.</param>
    /// <returns></returns>
    ValidationResult Validate<T>(T instance, string profileName);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    /// <param name="profileName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ValidationResult> ValidateAsync<T>(T instance, string profileName, CancellationToken cancellationToken);

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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ValidationResult> ValidateAsync(IValidationContext context, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="profileName">The name of the profile to use to validate the <see cref="IValidationContext.Instance"/>.</param>
    /// <returns></returns>
    ValidationResult Validate(IValidationContext context, string profileName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="profileName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ValidationResult> ValidateAsync(IValidationContext context, string profileName, CancellationToken cancellationToken);
}

