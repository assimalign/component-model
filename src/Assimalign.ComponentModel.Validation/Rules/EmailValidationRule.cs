using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    using Assimalign.ComponentModel.Validation.Abstraction;
    using Assimalign.ComponentModel.Validation.Exceptions;


    internal class EmailValidationRule<T, TValue> : IValidationRule, IValidationError
    {
        private readonly string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";
        private readonly Expression<Func<T, TValue>> expression;

        public EmailValidationRule(Expression<Func<T, TValue>> expression)
        {
            this.expression = expression;
            this.Name = "EmailValidationRule";
            this.Code = "400";
            this.Message = $"The following property, field, or collection '{expression.Body}' had an invalid email string.";
            this.Source = $"{expression}";
        }

        /// <summary>
        /// The name of the Validation Rule.
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
                var value = this.expression.Compile().Invoke(instance);

                if (value is IEnumerable<string> emails)
                {
                    foreach (var email in emails)
                    {
                        if (!Regex.IsMatch(email, pattern))
                        {
                            context.AddFailure(this);
                            break;
                        }
                    }
                }
                else if (value is string email)
                {
                    if (!Regex.IsMatch(email, pattern))
                    {
                        context.AddFailure(this);
                    }
                }
                else
                {
                    throw new ValidatorInvalidEvaluationException(
                        message: "The property, field, or collection type is not valid for Email Validation.",
                        source: $"{this.expression}");
                }
            }
            else
            {
                // TODO: An unknown error has occurred if the code has gotten this far. Need to figure out how to handle.
            }
        }
    }
}
