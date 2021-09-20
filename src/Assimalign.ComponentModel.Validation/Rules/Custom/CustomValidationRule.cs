using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    internal class CustomValidationRule<T> : ValidationRule<T>
    {

        /// <summary>
        /// 
        /// </summary>
        public Action<T, IValidatorContext<T>> Action {  get; set; }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="instance"></param>
        public override void Evaluate(IValidatorContext<T> context, T instance)
        {
            if (Action is not null)
            {
                Action(instance, context);
            }
        }
    }
}
