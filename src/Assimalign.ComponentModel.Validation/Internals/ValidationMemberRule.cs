using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Assimalign.ComponentModel.Validation.Internals
{
    using Assimalign.ComponentModel.Validation.Exceptions;
    using Assimalign.ComponentModel.Validation.Abstraction;

    internal sealed class ValidationMemberRule<T, TMember> : IValidationMemberRule<T, TMember>
    {
        private Expression<Func<T, TMember>> member;
        private readonly IList<IValidationRule> rules = new List<IValidationRule>();


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
        public string Validator { get; }

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
        public IEnumerable<IValidationRule> Rules => rules;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        public void AddRule(IValidationRule rule) =>
            rules.Add(rule);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Evaluate(IValidationContext context)
        {
            if (context.ValidationInstance is T instance)
            {
                Parallel.ForEach(this.Rules, rule =>
                {
                    rule.Evaluate(context);
                });
            }
        }
    }
}
