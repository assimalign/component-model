using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal;

using Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class ValidationMemberRuleBuilder<T, TValue> : IValidationRuleBuilder<T, TValue>
{

    public ValidationMemberRuleBuilder(IValidationMemberRule<T, TValue> rule)
    {
        MemberRule = rule;
    }

    /// <summary>
    /// 
    /// </summary>
    public IValidationMemberRule<T, TValue> MemberRule { get; }

    /// <summary>
    /// 
    /// </summary>
    IValidationRule IValidationRuleBuilder<T, TValue>.Current => MemberRule;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<T, TValue> Between<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : IComparable<TBound>
    {
        return Between<TBound>(lowerBound, upperBound, configure =>
        {
            configure.Message = $"One of the following items in '{string.Join('.', MemberRule.Member.Body.ToString().Split('.').Skip(1))}' is not within bounds of: {lowerBound} and {upperBound}.";
            configure.Source = MemberRule.Member.ToString();
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<T, TValue> Between<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure) where TBound : IComparable<TBound>
    {
        var error = new ValidationError();

        configure.Invoke(error);

        MemberRule.AddRule(new BetweenValidationRule<T, TValue, TBound>(MemberRule.Member, lowerBound, upperBound)
        {
            Error = error
        });

        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : IComparable<TBound>
    {
        return BetweenOrEqualTo<TBound>(lowerBound, upperBound, configure =>
        {
            configure.Message = "";
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)
        where TBound : IComparable<TBound>
    {
        var error = new ValidationError();

        configure.Invoke(error);

        MemberRule.AddRule(new BetweenOrEqualToValidationRule<T, TValue, TBound>(MemberRule.Member, lowerBound, upperBound)
        {
            Error = error
        });

        return this;
    }


    public IValidationRuleBuilder<T, TValue> ChildRules(Action<IValidationRuleDescriptor<TValue>> child)
    {
        
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="validation"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<T, TValue> Custom(Action<TValue, IValidationContext> validation)
    {
        MemberRule.AddRule(new CustomValidationRule<T, TValue>(MemberRule.Member, validation));

        return this;
    }

    public IValidationRuleBuilder<T, TValue> Empty()
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Equal<TNumber>(TNumber value) where TNumber : IComparable
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> GreaterThan<TNumber>(TNumber value) where TNumber : struct, IComparable
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> GreaterThan<TNumber>(TNumber value, Action<IValidationError> confiure) where TNumber : struct, IComparable
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> GreaterThanOrEqualTo<TNumber>(TNumber value) where TNumber : struct, IComparable
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Length(int min, int max)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Length(int exact)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Length(int min, int max, Action<IValidationError> configure)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Length(int exact, Action<IValidationError> configure)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> LessThan<TNumber>(TNumber value) where TNumber : struct, IComparable
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> LessThanOrEqualTo<TNumber>(TValue value) where TNumber : struct, IComparable
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> MaxLength(int max)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> MaxLength(int max, Action<IValidationError> configure)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> MinLength(int min)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> MinLength(int min, Action<IValidationError> configure)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> NotEmpty()
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> NotEqual<TNumber>(TNumber value) where TNumber : IComparable
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> NotNull()
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> NotNull(Action<IValidationError> configure)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Null()
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Null(Action<IValidationError> configure)
    {
        throw new NotImplementedException();
    }

    //public IValidationRuleBuilder<T, TValue> UseValidator(IValidator<TValue> validator)
    //{
    //    throw new NotImplementedException();
    //}

    public IValidationRuleBuilder<T, TValue> WithErrorCode(string code)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> WithErrorMessage(string message)
    {
        throw new NotImplementedException();
    }
}

