using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    internal class ValidationMemberRule<T> : IValidationMemberRule<T>
    {
        private readonly IList<IValidationRule> rules = new List<IValidationRule>();


        /// <summary>
        /// 
        /// </summary>
        public MemberExpression Member { get; set; }

        public LambdaExpression MemberDelegate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
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
        public void AddMemberRule(IValidationRule rule) =>
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
