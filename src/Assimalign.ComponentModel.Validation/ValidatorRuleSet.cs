using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{

    using Assimalign.ComponentModel.Validation.Rules;


    public sealed class ValidatorRuleSet<T> : IValidatorRuleSet<T>
    {
        private IValidationRule<T>[] rules;

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

        public Func<T, bool> RunWhen { get; internal set; }

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
