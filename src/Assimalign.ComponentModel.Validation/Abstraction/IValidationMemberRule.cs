﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Abstraction
{
    /// <summary>
    /// Represents a validation rule for a property or field of a given type.
    /// </summary>
    /// <typeparam name="T">Is the type in which the property or field is a member of.</typeparam>
    /// <typeparam name="TMember">Is either a property or member of type 'T'.</typeparam>
    public interface IValidationMemberRule<T, TMember> : IValidationRule
    {
        /// <summary>
        /// 
        /// </summary>
        Expression<Func<T, TMember>> Member { get; }
        

        /// <summary>
        /// The validation rules to apply to the member.
        /// </summary>
        IEnumerable<IValidationRule> Rules { get; }

        /// <summary>
        /// A member validator to be used on top of any additional rules  
        /// </summary>
        IValidator<TMember> Validator { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        IValidationMemberRule<T, TMember> AddRule(IValidationRule rule);
    }
}
