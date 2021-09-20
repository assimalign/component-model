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
    

    public sealed class ValidatorRuleSet<T> : IValidatorRuleSet<T>
    {

        private IValidationRule<T>[] rules;

        public ValidatorRuleSet()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IValidationRule<T> this[int index]
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
        public void Evaluate(IValidatorContext<T> context, T instance)
        {
            var loopCanceller = new CancellationTokenSource();
            var loopOptions = new ParallelOptions()
            {
                 CancellationToken = loopCanceller.Token
            };

            // Let's loop through these suckers real fast with some multi-threading
            Parallel.ForEach(this, rule =>
            {
                rule.Evaluate(context, instance);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(IValidationRule<T> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Array.Clear(rules, 0, rules.Length);
        }

        public bool Contains(IValidationRule<T> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IValidationRule<T>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

       

        public IEnumerator<IValidationRule<T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(IValidationRule<T> item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IValidationRule<T> item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IValidationRule<T> item)
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
