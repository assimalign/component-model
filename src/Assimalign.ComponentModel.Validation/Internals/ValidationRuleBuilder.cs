using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internals
{
    using Assimalign.ComponentModel.Validation.Abstraction;


    internal sealed class ValidationRuleBuilder<T, TValue> : IValidationRuleBuilder<T, TValue>
    {
        public ValidationRuleBuilder(IValidationRule current)
        {
            this.Current = current;
        }

        /// <summary>
        /// The current rul
        /// </summary>
        public IValidationRule Current { get; set; }


        public IValidationRuleBuilder<T, TValue> Between<TLowerBound, TUpperBound>(TLowerBound left, TUpperBound right)
            where TLowerBound : struct, IComparable<TValue>
            where TUpperBound : struct, IComparable<TValue>
        {
            throw new NotImplementedException();
        }

        public IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TLowerBound, TUpperBound>(TLowerBound left, TUpperBound right)
            where TLowerBound : struct, IComparable<TValue>
            where TUpperBound : struct, IComparable<TValue>
        {
            throw new NotImplementedException();
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

        public IValidationRuleBuilder<T, TValue> Equal<TNumber>(TNumber value) where TNumber : IComparable<TValue>
        {
            throw new NotImplementedException();
        }

        public IValidationRuleBuilder<T, TValue> GreaterThan<TNumber>(TNumber value) where TNumber : struct, IComparable<TValue>
        {
            throw new NotImplementedException();
        }

        public IValidationRuleBuilder<T, TValue> GreaterThanOrEqualTo<TNumber>(TNumber value) where TNumber : struct, IComparable<TValue>
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

        public IValidationRuleBuilder<T, TValue> LessThan<TNumber>(TNumber value) where TNumber : struct, IComparable<TValue>
        {
            throw new NotImplementedException();
        }

        public IValidationRuleBuilder<T, TValue> LessThanOrEqualTo<TNumber>(TValue value) where TNumber : struct, IComparable<TValue>
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

        public IValidationRuleBuilder<T, TValue> NotEqual<TNumber>(TNumber value) where TNumber : IComparable<TValue>
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
