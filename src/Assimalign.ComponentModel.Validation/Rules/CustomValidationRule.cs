using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    using Assimalign.ComponentModel.Validation.Abstraction;


    internal class CustomValidationRule<T> : IValidationRule
    {


        public CustomValidationRule(Action<T, IValidationContext> action)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public Action<T, IValidationContext> Action {  get; set; }

        public string Name { get; }

        public string Message { get; set; }




        public void Evaluate(IValidationContext context)
        {
            if (context.ValidationInstance is T instance)
            {
                Action.Invoke(instance, context);
            }
        }
    }
}
