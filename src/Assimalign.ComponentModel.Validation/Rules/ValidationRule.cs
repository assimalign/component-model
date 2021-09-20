using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    using Assimalign.ComponentModel.Validation.Exceptions;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class ValidationRule<T> : IValidationRule<T>
    {
        private Expression validation;

        /// <summary>
        /// 
        /// </summary>
        protected ValidationRule()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Validator { get; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public virtual LambdaExpression Validation
        {
            get => validation as LambdaExpression;
            set
            {
                if (value is LambdaExpression lambda && lambda.ReturnType == typeof(bool))
                {
                    validation = lambda;
                } 
                else
                {
                    throw new ValidatorPredicateException($"The predicate for Validation Rule '{this.Name}' is " +
                        $"either not a LambdaExpression or does not have a return type of 'bool'.");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="instance"></param>
        public virtual void Evaluate(IValidatorContext<T> context, T instance)
        {
            throw new NotImplementedException();
        }
    }
}
