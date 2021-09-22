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


    internal class EmailValidationRule<T, TValue> : IValidationRule
    {
        private readonly string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";
        private readonly string name;
        private readonly Func<T, TValue> outputValue;

        public EmailValidationRule(Expression<Func<T, TValue>> lambda)
        {
            this.name = "EmailValidationRule";
            this.outputValue = lambda.Compile();

            this.Message = "";
        }

        public MemberInfo Member { get; set; }

        public string Validator { get; }

        public string Name { get; }

        public string Message { get; set; }

        public void Evaluate(IValidationContext context)
        {
            if (context.ValidationInstance is T instance)
            {
                var value = this.outputValue.Invoke(instance);
                if (value is IEnumerable<string> emails)
                {
                    foreach(var email in emails)
                    {
                        if (!Regex.IsMatch(email, pattern))
                        {
                            context.AddFailure(new ValidationError()
                            {
                                Message = this.Message
                            });
                            break;
                        }
                    }
                }
                else if (value is string email)
                {
                    if (!Regex.IsMatch(email, pattern))
                    {
                        context.AddFailure(new ValidationError()
                        {
                            Message = this.Message
                        });
                    }
                }
            }
        }
    }
}
