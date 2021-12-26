using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
   


    internal class NotEmptyValidationRule<T, TValue> : IValidationRule, IValidationError
    {

        private readonly Expression<Func<T, TValue>> expression;

        public NotEmptyValidationRule(Expression<Func<T, TValue>> expression)
        {
            this.expression = expression;

            this.Name = "NotEmptyValidationRule";
            this.Code = "400";
            this.Message = $"The following property, field, or collection '{expression.Body}' was identified as empty.";
            this.Source = $"{expression}";
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A unique error code to use when the validation rule fails.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// A unique error message to use when the validation rule fails.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The source of the validation failure.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Evaluate(IValidationContext context)
        {
            if (context.Instance is T instance)
            {
                var value = expression.Compile().Invoke(instance);

                if (IsEmpty(value))
                {
                    context.AddFailure(this);
                }
            }
            else
            {
                // TODO: 
            }
        }


        private bool IsEmpty(TValue member)
        {
            switch (member)
            {
                case null:
                case string stringValue when string.IsNullOrWhiteSpace(stringValue):
                case ICollection { Count: 0 }:
                case Array { Length: 0 } c:
                case IEnumerable e when !e.Cast<object>().Any():
                    return true;
            }

            return false;
        }
    }
}
