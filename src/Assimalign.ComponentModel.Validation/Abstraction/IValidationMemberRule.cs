﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TMember"></typeparam>
    public interface IValidationMemberRule<T, TMember> : IValidationRule
    {

        /// <summary>
        /// The member of Type 'T' being evaluated for the rule.
        /// </summary>
        /// <remarks>
        /// If the member expression is being set from a Lambda Expression (example: x => x.Member) then 
        /// the member is the Body (x.Member).
        /// </remarks>
        MemberExpression Member { get; }

        /// <summary>
        /// 
        /// </summary>
        Expression<Func<T, TMember>> MemberDelegate { get; set; }

        /// <summary>
        /// The validation rules to apply to the member.
        /// </summary>
        IEnumerable<IValidationRule> Rules { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        void AddRule(IValidationRule rule);
    }
}
