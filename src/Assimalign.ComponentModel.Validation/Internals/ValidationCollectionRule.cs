using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internals
{
    using Assimalign.ComponentModel.Validation.Abstraction;
    

    internal sealed class ValidationCollectionRule<T, TCollection> : IValidationCollectionRule<T, TCollection>
        where TCollection : IEnumerable
    {
        private IList<IValidationRule> rules = new List<IValidationRule>();

        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<T, TCollection>> Collection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IValidationRuleSet Rules { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        public IValidator<TCollection> Validator { get; set; }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        public IValidationCollectionRule<T, TCollection> AddRule(IValidationRule rule) 
        {
            rules.Add(rule);
            return this;
        }

        public IValidationCollectionRule<T, TCollection> Empty()
        {
            throw new NotImplementedException();
        }

        public IValidationCollectionRule<T, TCollection> Equal<TValue>(TValue value) where TValue : IComparable
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Evaluate(IValidationContext context)
        {
            if (context.ValidationInstance is T instance)
            {
                var values = this.Collection.Compile();

                foreach(var value in values.Invoke(instance))
                {

                }
            }

            throw new NotImplementedException();
        }

        public IValidationCollectionRule<T, TCollection> GreaterThan<TValue>(TValue value) where TValue : struct
        {
            throw new NotImplementedException();
        }

        public IValidationCollectionRule<T, TCollection> GreaterThanOrEqualTo<TValue>(TValue value) where TValue : struct
        {
            throw new NotImplementedException();
        }

        public IValidationCollectionRule<T, TCollection> LessThan<TValue>(TValue value) where TValue : struct
        {
            throw new NotImplementedException();
        }

        public IValidationCollectionRule<T, TCollection> LessThanOrEqualTo<TValue>(TValue value) where TValue : struct
        {
            throw new NotImplementedException();
        }

        public IValidationCollectionRule<T, TCollection> NotEmpty()
        {
            throw new NotImplementedException();
        }

        public IValidationCollectionRule<T, TCollection> NotEqual<TValue>(TValue value) where TValue : IComparable
        {
            throw new NotImplementedException();
        }

        public IValidationCollectionRule<T, TCollection> UseValidator(IValidator<TCollection> validator)
        {
            throw new NotImplementedException();
        }


        public IValidationMemberRule<T, TCollection> Between<TLeftBound, TRightBound>(TLeftBound left, TRightBound right)
            where TLeftBound : struct
            where TRightBound : struct
        {
            throw new NotImplementedException();
        }

        public IValidationMemberRule<T, TCollection> BetweenOrEqualTo<TLeftBound, TRightBound>(TLeftBound left, TRightBound right)
            where TLeftBound : struct
            where TRightBound : struct
        {
            throw new NotImplementedException();
        }

        public IValidationCollectionRule<T, TCollection> OneOf(Action<IValidationRule> evaluations)
        {
            throw new NotImplementedException();
        }

        public IValidationCollectionRule<T, TCollection> Null()
        {
            throw new NotImplementedException();
        }

        public IValidationCollectionRule<T, TCollection> NotNull()
        {
            throw new NotImplementedException();
        }
    }
}
