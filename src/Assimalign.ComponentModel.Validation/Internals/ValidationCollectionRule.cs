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

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        public void AddRule(IValidationRule rule) => rules.Add(rule);

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
    }
}
