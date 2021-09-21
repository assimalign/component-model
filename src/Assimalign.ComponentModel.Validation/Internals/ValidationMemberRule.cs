using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    internal class ValidationMemberRule<T, TMember> : IValidationMemberRule<T, TMember>
    {
        private readonly IList<IValidationRule> rules = new List<IValidationRule>();


        /// <summary>
        /// 
        /// </summary>
        public MemberExpression Member => 
            MemberDelegate.Body as MemberExpression;

        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<T, TMember>> MemberDelegate { get; set; }
        
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
