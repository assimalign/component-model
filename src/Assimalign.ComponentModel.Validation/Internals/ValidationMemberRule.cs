using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Assimalign.ComponentModel.Validation.Internals
{
    using Assimalign.ComponentModel.Validation.Rules;
    using Assimalign.ComponentModel.Validation.Exceptions;
    using Assimalign.ComponentModel.Validation.Abstraction;

    internal sealed class ValidationMemberRule<T, TMember> : IValidationMemberRule<T, TMember>
    {
        // long - index of rule | string - message for indexed rule
        private readonly IDictionary<long, string> codes = new Dictionary<long, string>();
        private readonly IDictionary<long, string> messages = new Dictionary<long, string>();


        private Expression<Func<T, TMember>> member;
        private readonly Stack<IValidationRule> rules = new Stack<IValidationRule>();


        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<T, TMember>> Member
        {
            get => member;
            set
            {
                if (value.Body is MemberExpression)
                {
                    this.member = value;
                }
                else
                {
                    throw new ValidatorMemberException();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IValidator<TMember> MemberValidator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IValidationRule> Rules => rules;

        /// <summary>
        /// 
        /// </summary>
        public IValidator<TMember> Validator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        public IValidationMemberRule<T, TMember> AddRule(IValidationRule rule)
        {
            rules.Push(rule);
            return this;
        }
            

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Evaluate(IValidationContext context)
        {
            if (context.Instance is T instance)
            {
                Parallel.ForEach(this.Rules, (rule, state, index) =>
                {
                    if (codes.TryGetValue(index, out var code))
                    {

                    }

                    if (messages.TryGetValue(index, out var message))
                    {

                    }
                    
                    rule.Evaluate(context);
                });
            }
        }
    }
}
