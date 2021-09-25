using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internals
{
    using Assimalign.ComponentModel.Validation.Rules;
    using Assimalign.ComponentModel.Validation.Internals;
    using Assimalign.ComponentModel.Validation.Abstraction;
    

    internal sealed class ValidationMemberRuleBuilder<T, TValue> : IValidationRuleBuilder<T, TValue>
    {

        public ValidationMemberRuleBuilder(IValidationMemberRule<T, TValue> rule)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public IValidationMemberRule<T, TValue> MemberRule { get; }
        IValidationRule IValidationRuleBuilder<T, TValue>.Current => MemberRule;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBound"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public IValidationRuleBuilder<T, TValue> Between<TBound>(TBound left, TBound right)
            where TBound :  IComparable<TBound>
        {
            MemberRule.AddRule(new BetweenValidationRule<T, TValue, TBound>(MemberRule.Member, left, right));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBound"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TBound>(TBound left, TBound right)
            where TBound : IComparable<TBound>
        {
            MemberRule.AddRule(new BetweenOrEqualToValidationRule<T, TValue, TBound>(MemberRule.Member, left, right));
            return this;
        }

        public IValidationRuleBuilder<T, TValue> ChildRules(Action<IValidationRuleInitializer<TValue>> child)
        {
            throw new NotImplementedException();
        }

        public IValidationRuleBuilder<T, TValue> Custom(Action<TValue, IValidationContext> validation)
        {
            throw new NotImplementedException();
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

        public IValidationRuleBuilder<T, TValue> MinLength(int min)
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

        public IValidationRuleBuilder<T, TValue> Null()
        {
            throw new NotImplementedException();
        }

        public IValidationRuleBuilder<T, TValue> UseValidator(IValidator<TValue> validator)
        {
            throw new NotImplementedException();
        }

        public IValidationRuleBuilder<T, TValue> WithErrorCode(string code)
        {
            throw new NotImplementedException();
        }

        public IValidationRuleBuilder<T, TValue> WithErrorMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
