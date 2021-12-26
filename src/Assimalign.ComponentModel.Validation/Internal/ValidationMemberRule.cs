using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Assimalign.ComponentModel.Validation.Internal
{
    using Assimalign.ComponentModel.Validation.Rules;
    using Assimalign.ComponentModel.Validation.Internal.Exceptions;

    internal sealed class ValidationMemberRule<T, TMember> : IValidationMemberRule<T, TMember>
    {

        private Expression<Func<T, TMember>> member;

        public ValidationMemberRule()
        {
            this.MemberRules ??= new ValidationRuleStack();
        }



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
                    throw new ValidationMemberException();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public IValidationRuleStack MemberRules { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        public IValidationMemberRule<T, TMember> AddRule(IValidationRule rule)
        {
            MemberRules.Push(rule);
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
                Parallel.ForEach(this.MemberRules, (rule, state, index) =>
                {                   
                    rule.Evaluate(context);
                });
            }
        }
    }
}
