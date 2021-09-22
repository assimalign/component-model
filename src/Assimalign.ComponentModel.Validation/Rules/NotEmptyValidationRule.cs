using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    using Assimalign.ComponentModel.Validation.Abstraction;


    internal class NotEmptyValidationRule<T, TValue> : IValidationRule
    {
        private readonly string name;
        private readonly Func<T, TValue> outputValue;

        public NotEmptyValidationRule(Expression<Func<T, TValue>> lambda)
        {
            this.outputValue = lambda.Compile();
            this.name = "NotEmptyValidationRule";
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name => name;

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Evaluate(IValidationContext context)
        {
            if (context.ValidationInstance is T instance)
            {
                var value = outputValue.Invoke(instance);

                if (IsEmpty(value))
                {
                    context.AddFailure(new ValidationError()
                    {

                    });
                }
            }
        }


        private bool IsEmpty(TValue member)
        {
            switch (member)
            {
                case null:
                case string s when string.IsNullOrWhiteSpace(s):
                case ICollection { Count: 0 }:
                case Array { Length: 0 } c:
                case IEnumerable e when !e.Cast<object>().Any():
                    return true;
            }

            return false;
        }
    }
}
