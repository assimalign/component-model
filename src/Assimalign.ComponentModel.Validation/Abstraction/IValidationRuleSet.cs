﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Abstraction
{
    using Assimalign.ComponentModel.Validation.Rules;


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationRuleSet : ICollection<IValidationRule>, IList<IValidationRule>
    {
        /// <summary>
        /// Evaluates the collection of
        /// </summary>
        void Evaluate(IValidationContext context);
    }
}
