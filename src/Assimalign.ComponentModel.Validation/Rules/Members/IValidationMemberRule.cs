using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    public interface IValidationMemberRule<T> : IValidationRule<T>
    {

        /// <summary>
        /// The member being evaluated for the rule
        /// </summary>
        string Member { get; set; }

    }
}
