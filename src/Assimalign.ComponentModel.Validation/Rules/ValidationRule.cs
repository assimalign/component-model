using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    public abstract class ValidationRule<T> : IValidationRule<T>
    {
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
        public virtual Func<T, bool> Rule { get; set;  }

        /// <summary>
        /// 
        /// </summary>
        public virtual ValidationError Error { get; set; } = new ValidationError();

        /// <summary>
        /// 
        /// </summary>
        LambdaExpression IValidationRule<T>.Rule { get; set; }
    }


    internal sealed class ValidatorRuleDefault<T> : ValidationRule<T>
    {


        public static IValidationRule<T> Create(Expression<Func<T, bool>> expression)
        {
            return new ValidationRuleDefault<T>(expression);

        }
    }
}
