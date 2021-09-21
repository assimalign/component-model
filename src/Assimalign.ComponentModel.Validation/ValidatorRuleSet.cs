using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;



namespace Assimalign.ComponentModel.Validation
{

    using Assimalign.ComponentModel.Validation.Rules;
    
    /// <summary>
    /// A rule set is a collection of validation rules to be used to 
    /// when validating the instance.
    /// </summary>
    public sealed class ValidatorRuleSet : IValidatorRuleSet
    {

        private IValidationRule[] rules;

        public ValidatorRuleSet()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IValidationRule this[int index]
        {
            get => rules[index];
            set => rules[index] = value;
        }


        /// <summary>
        /// 
        /// </summary>
        public int Count => rules.Length;

        public bool IsReadOnly => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="instance"></param>
        public void Evaluate(IValidationContext context)
        {
            var loopCanceller = new CancellationTokenSource();
            var loopOptions = new ParallelOptions()
            {
                 CancellationToken = loopCanceller.Token
            };

            // Let's loop through these suckers real fast with some multi-threading
            Parallel.ForEach(this, rule =>
            {
                rule.Evaluate(context);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(IValidationRule item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Array.Clear(rules, 0, rules.Length);
        }

        public bool Contains(IValidationRule item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IValidationRule[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

       

        public IEnumerator<IValidationRule> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(IValidationRule item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IValidationRule item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IValidationRule item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
