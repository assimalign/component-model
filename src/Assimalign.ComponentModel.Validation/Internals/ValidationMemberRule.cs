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
            if (context.ValidationInstance is T instance)
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

        public IValidationMemberRule<T, TMember> NotEmpty()
        {
            var validator = new NotEmptyValidationRule<T, TMember>(this.Member);
            AddRule(validator);
            return this;
        }

        public IValidationMemberRule<T, TMember> Empty()
        {
            throw new NotImplementedException();
        }

        public IValidationMemberRule<T, TMember> GreaterThan<TValue>(TValue value) where TValue : struct
        {
            throw new NotImplementedException();
        }

        public IValidationMemberRule<T, TMember> GreaterThanOrEqualTo<TValue>(TValue value) where TValue : struct
        {
            throw new NotImplementedException();
        }

        public IValidationMemberRule<T, TMember> LessThan<TValue>(TValue value) where TValue : struct
        {
            throw new NotImplementedException();
        }

        public IValidationMemberRule<T, TMember> LessThanOrEqualTo<TValue>(TValue value) where TValue : struct
        {
            throw new NotImplementedException();
        }

        public IValidationMemberRule<T, TMember> Equal<TValue>(TValue value) where TValue : IComparable
        {
            throw new NotImplementedException();
        }

        public IValidationMemberRule<T, TMember> NotEqual<TValue>(TValue value) where TValue : IComparable
        {
            throw new NotImplementedException();
        }

        public IValidationMemberRule<T, TMember> UseValidator(IValidator<TMember> validator)
        {
            throw new NotImplementedException();
        }

        IValidationMemberRule<T, TMember> IValidationMemberRule<T, TMember>.AddRule(IValidationRule rule)
        {
            throw new NotImplementedException();
        }


        public IValidationMemberRule<T, TMember> WithErrorCode(string code)
        {
            codes.Add(this.rules.Count, code);
            return this;
        }

        public IValidationMemberRule<T, TMember> WithErrorMessage(string message)
        {
            messages.Add(this.rules.Count, message);
            return this;
        }

        public IValidationMemberRule<T, TMember> Between<TLeftBound, TRightBound>(TLeftBound left, TRightBound right)
            where TLeftBound : struct
            where TRightBound : struct
        {
            throw new NotImplementedException();
        }

        public IValidationMemberRule<T, TMember> BetweenOrEqualTo<TLeftBound, TRightBound>(TLeftBound left, TRightBound right)
            where TLeftBound : struct
            where TRightBound : struct
        {
            throw new NotImplementedException();
        }

        public IValidationMemberRule<T, TMember> Null()
        {
            throw new NotImplementedException();
        }

        public IValidationMemberRule<T, TMember> NotNull()
        {
            throw new NotImplementedException();
        }
    }
}
