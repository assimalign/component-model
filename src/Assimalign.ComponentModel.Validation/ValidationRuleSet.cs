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
    using Assimalign.ComponentModel.Validation.Abstraction;
    
    /// <summary>
    /// A rule set is a collection of validation rules to be used to 
    /// when validating the instance.
    /// </summary>
    public sealed class ValidationRuleSet : IValidationRuleSet
    {
        private int size;
        private int version;
        private IValidationRule[] rules;

        public ValidationRuleSet()
        {
            rules = new IValidationRule[0];
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

        /// <summary>
        /// 
        /// </summary>
        public int Capacity
        {
            get
            {
                return this.rules.Length;
            }
            set
            {
                if (value < this.size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                if (value == this.rules.Length)
                {
                    return;
                }
                if (value > 0)
                {
                    var array = new IValidationRule[value];
                    if (this.size > 0)
                    {
                        Array.Copy(this.rules, array, this.size);
                    }
                    this.rules = array;
                }
                else
                {
                    this.rules = new IValidationRule[0];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Evaluate(IValidationContext context)
        {
            var loopCanceller = new CancellationTokenSource();
            var loopOptions = new ParallelOptions()
            {
                 CancellationToken = loopCanceller.Token
            };

            // Let's loop through these suckers real fast with some multi-threading
            var loopResults = Parallel.ForEach(this, loopOptions, rule =>
            {
                rule.Evaluate(context);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(IValidationRule rule)
        {
            version++;
            IValidationRule[] rules = this.rules;
            int size = this.size;
            if ((uint)size < (uint)this.rules.Length)
            {
                this.size = size + 1;
                rules[size] = rule;
            }
            else
            {
                AddWithResizing(rule);
            }
        }

        private void AddWithResizing(IValidationRule rule)
        {
            int size = this.size;
            EnsureCapacity(size + 1);
            this.size = size + 1;
            this.rules[size] = rule;
        }

        private void EnsureCapacity(int min)
        {
            if (this.rules.Length < min)
            {
                int num = ((this.rules.Length == 0) ? 4 : (this.rules.Length * 2));
                if ((uint)num > 2146435071u)
                {
                    num = 2146435071;
                }
                if (num < min)
                {
                    num = min;
                }
                Capacity = num;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear() => Array.Clear(rules, 0, rules.Length);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(IValidationRule item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(IValidationRule[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(IValidationRule item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, IValidationRule item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(IValidationRule item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IValidationRule> GetEnumerator()
        {
            return new ValueRuleSetEnumerator(rules);
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ValueRuleSetEnumerator(rules);
        }




        internal struct ValueRuleSetEnumerator : IEnumerator<IValidationRule>
        {
            private int current;
            private IValidationRule[] rules;

            public ValueRuleSetEnumerator(IValidationRule[] rules)
            {
                this.rules = rules;
                this.current = -1;
            }


            public IValidationRule Current => rules[current];

            object IEnumerator.Current => this.Current;

            public void Dispose()
            {
                current = -1;
                rules = new IValidationRule[0];
            }

            public bool MoveNext()
            {
                current++;
                if (current < rules.Length)
                {
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                current = -1;
            }
        }
    }
}
